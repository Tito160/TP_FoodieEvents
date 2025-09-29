using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tp_EventoComida;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Servicios necesarios para Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Habilitar Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// 🔹 Middleware global de manejo de errores
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

// 🔹 Middleware básico
app.UseHttpsRedirection();

// ⚡ Datos en memoria (simulación de base de datos)
var chefs = new List<Chef>();
var eventos = new List<EventoGastronomico>();
var participantes = new List<Participante>();
var reservas = new List<Reserva>();

// ==========================
// 🔹 ENDPOINTS CHEF
// ==========================
app.MapPost("/chefs", (Chef chef) =>
{
    chefs.Add(chef);
    return Results.Created($"/chefs/{chef.Id}", chef);
});

app.MapGet("/chefs", () => Results.Ok(chefs));

app.MapGet("/chefs/{id}", (int id) =>
{
    var chef = chefs.FirstOrDefault(c => c.Id == id);
    return chef is not null ? Results.Ok(chef) : Results.NotFound();
});

// ==========================
// 🔹 ENDPOINTS EVENTO
// ==========================
app.MapPost("/eventos", (EventoGastronomico evento) =>
{
    eventos.Add(evento);
    return Results.Created($"/eventos/{evento.Id}", evento);
});

app.MapGet("/eventos", () => Results.Ok(eventos));

app.MapGet("/eventos/{id}", (int id) =>
{
    var evento = eventos.FirstOrDefault(e => e.Id == id);
    return evento is not null ? Results.Ok(evento) : Results.NotFound();
});

app.MapDelete("/eventos/{id}", (int id) =>
{
    var evento = eventos.FirstOrDefault(e => e.Id == id);
    if (evento is null) return Results.NotFound();

    // ❗ Política: si se elimina evento, también se eliminan reservas
    reservas.RemoveAll(r => r.Evento.Id == id);
    eventos.Remove(evento);

    return Results.NoContent();
});

// ==========================
// 🔹 ENDPOINTS PARTICIPANTE
// ==========================
app.MapPost("/participantes", (Participante participante) =>
{
    participantes.Add(participante);
    return Results.Created($"/participantes/{participante.Id}", participante);
});

app.MapGet("/participantes", () => Results.Ok(participantes));

// ==========================
// 🔹 ENDPOINTS RESERVA
// ==========================
app.MapPost("/reservas", (int participanteId, int eventoId) =>
{
    var participante = participantes.FirstOrDefault(p => p.Id == participanteId);
    var evento = eventos.FirstOrDefault(e => e.Id == eventoId);

    if (participante is null || evento is null)
        return Results.BadRequest("Participante o Evento no encontrado.");

    // ❗ Validación: evento no lleno
    if (reservas.Count(r => r.Evento.Id == eventoId && r.Estado != "Cancelada") >= evento.CapacidadMaxima)
        return Results.BadRequest("El evento ya está lleno.");

    var reserva = new Reserva(reservas.Count + 1, participante, evento);
    reservas.Add(reserva);

    return Results.Created($"/reservas/{reserva.Id}", reserva);
});

app.MapGet("/reservas", () => Results.Ok(reservas));

app.MapPut("/reservas/{id}/pago", (int id, string metodoPago) =>
{
    var reserva = reservas.FirstOrDefault(r => r.Id == id);
    if (reserva is null) return Results.NotFound();

    reserva.ConfirmarPago(metodoPago);
    return Results.Ok(reserva);
});

app.MapPut("/reservas/{id}/cancelar", (int id) =>
{
    var reserva = reservas.FirstOrDefault(r => r.Id == id);
    if (reserva is null) return Results.NotFound();

    reserva.CancelarReserva();
    return Results.Ok(reserva);
});
app.MapPost("/chefs", (Chef chef) =>
{
    ValidadorDatos.ValidarEmail(chef.Email);
    ValidadorDatos.ValidarTelefono(chef.Telefono);
    chefs.Add(chef);
    return Results.Created($"/chefs/{chef.Id}", chef);
});

app.MapPost("/chefs", (Chef chef) =>
{
    try
    {
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

// ==========================

app.Run();
