using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tp_EventoComida;

// 🔹 INICIALIZACIÓN: Crea el constructor de la aplicación web ASP.NET Core
var builder = WebApplication.CreateBuilder(args);

// 🔹 SWAGGER SERVICES: Agrega servicios para documentación automática de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 CONSTRUCCIÓN: Compila la aplicación con todos los servicios configurados
var app = builder.Build();

// 🔹 SWAGGER UI: Habilita la interfaz de Swagger solo en entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 MIDDLEWARE ERRORES: Manejo global de excepciones personalizadas
app.Use(async (context, next) =>
{
    try
    {
        await next(); // Ejecuta el siguiente middleware/endpoint
    }
    catch (ErrorValidacionException ex) // Captura nuestras excepciones personalizadas
    {
        context.Response.StatusCode = 400; // Bad Request
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            error = ex.Message
        });
    }
    catch (Exception ex) // Captura cualquier otra excepción
    {
        context.Response.StatusCode = 500; // Internal Server Error
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            error = "Ocurrió un error inesperado en el servidor.",
            detalles = ex.Message
        });
    }
});

// 🔹 SEGURIDAD: Redirección automática a HTTPS para conexiones seguras
app.UseHttpsRedirection();

// 🔹 BASE DE DATOS EN MEMORIA: Listas que simulan tablas de base de datos
var chefs = new List<Chef>();
var eventos = new List<EventoGastronomico>();
var participantes = new List<Participante>();
var reservas = new List<Reserva>();

// ==========================
// 🔹 ENDPOINTS CHEF - CRUD completo para gestión de chefs
// ==========================
app.MapPost("/chefs", (Chef chef) =>
{
    // CREATE: Agrega un nuevo chef a la lista
    chefs.Add(chef);
    return Results.Created($"/chefs/{chef.Id}", chef);
});

app.MapGet("/chefs", () => Results.Ok(chefs)); // READ: Obtiene todos los chefs

app.MapGet("/chefs/{id}", (int id) =>
{
    // READ BY ID: Busca un chef específico por su ID
    var chef = chefs.FirstOrDefault(c => c.Id == id);
    return chef is not null ? Results.Ok(chef) : Results.NotFound();
});

// ==========================
// 🔹 ENDPOINTS EVENTO - CRUD para eventos gastronómicos
// ==========================
app.MapPost("/eventos", (EventoGastronomico evento) =>
{
    // CREATE: Crea un nuevo evento gastronómico
    eventos.Add(evento);
    return Results.Created($"/eventos/{evento.Id}", evento);
});

app.MapGet("/eventos", () => Results.Ok(eventos)); // READ: Lista todos los eventos

app.MapGet("/eventos/{id}", (int id) =>
{
    // READ BY ID: Obtiene un evento específico
    var evento = eventos.FirstOrDefault(e => e.Id == id);
    return evento is not null ? Results.Ok(evento) : Results.NotFound();
});

app.MapDelete("/eventos/{id}", (int id) =>
{
    // DELETE: Elimina un evento y sus reservas asociadas (eliminación en cascada)
    var evento = eventos.FirstOrDefault(e => e.Id == id);
    if (evento is null) return Results.NotFound();

    // ❗ Política: si se elimina evento, también se eliminan reservas
    reservas.RemoveAll(r => r.Evento.Id == id);
    eventos.Remove(evento);

    return Results.NoContent();
});

// ==========================
// 🔹 ENDPOINTS PARTICIPANTE - Gestión de participantes
// ==========================
app.MapPost("/participantes", (Participante participante) =>
{
    // CREATE: Registra un nuevo participante
    participantes.Add(participante);
    return Results.Created($"/participantes/{participante.Id}", participante);
});

app.MapGet("/participantes", () => Results.Ok(participantes)); // READ: Lista participantes

// ==========================
// 🔹 ENDPOINTS RESERVA - Sistema completo de reservas
// ==========================
app.MapPost("/reservas", (int participanteId, int eventoId) =>
{
    // CREATE: Crea una nueva reserva con validaciones de negocio
    var participante = participantes.FirstOrDefault(p => p.Id == participanteId);
    var evento = eventos.FirstOrDefault(e => e.Id == eventoId);

    // Validación: existencia de participante y evento
    if (participante is null || evento is null)
        return Results.BadRequest("Participante o Evento no encontrado.");

    // ❗ Validación: evento no lleno (control de capacidad)
    if (reservas.Count(r => r.Evento.Id == eventoId && r.Estado != "Cancelada") >= evento.CapacidadMaxima)
        return Results.BadRequest("El evento ya está lleno.");

    var reserva = new Reserva(reservas.Count + 1, participante, evento);
    reservas.Add(reserva);

    return Results.Created($"/reservas/{reserva.Id}", reserva);
});

app.MapGet("/reservas", () => Results.Ok(reservas)); // READ: Lista todas las reservas

app.MapPut("/reservas/{id}/pago", (int id, string metodoPago) =>
{
    // UPDATE: Confirma el pago de una reserva
    var reserva = reservas.FirstOrDefault(r => r.Id == id);
    if (reserva is null) return Results.NotFound();

    reserva.ConfirmarPago(metodoPago);
    return Results.Ok(reserva);
});

app.MapPut("/reservas/{id}/cancelar", (int id) =>
{
    // UPDATE: Cancela una reserva (cambio de estado)
    var reserva = reservas.FirstOrDefault(r => r.Id == id);
    if (reserva is null) return Results.NotFound();

    reserva.CancelarReserva();
    return Results.Ok(reserva);
});

// 🔹 VALIDACIÓN EXTENDIDA: Endpoint con validaciones personalizadas para chefs
app.MapPost("/chefs", (Chef chef) =>
{
    try
    {
        // Valida formato de email y teléfono antes de guardar
        ValidadorDatos.ValidarEmail(chef.Email);
        ValidadorDatos.ValidarTelefono(chef.Telefono);
        chefs.Add(chef);
        return Results.Created($"/chefs/{chef.Id}", chef);
    }
    catch (ErrorValidacionException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// 🔹 INICIO SERVIDOR: Pone la aplicación en escucha de requests HTTP
app.Run();