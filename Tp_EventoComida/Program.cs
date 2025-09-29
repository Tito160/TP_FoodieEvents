using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tp_EventoComida;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (ErrorValidacionException ex)
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            error = "Ocurrió un error inesperado en el servidor.",
            detalles = ex.Message
        });
    }
});

app.UseHttpsRedirection();

// 🔹 BASE DE DATOS EN MEMORIA ACTUALIZADA
var chefs = new List<Chef>();
var participantes = new List<Participante>();
var invitadosEspeciales = new List<InvitadoEspecial>();
var eventos = new List<EventoBase>(); // Cambiado a EventoBase
var reservas = new List<Reserva>();

// 🔹 INICIALIZAR SERVICIO DE INFORMES
var servicioInformes = new ServicioInformes(
    eventos,
    chefs.Cast<Persona>().Concat(participantes.Cast<Persona>()).Concat(invitadosEspeciales).ToList(),
    reservas
);

// ==========================
// 🔹 ENDPOINTS PERSONAS - Trabajan con interfaz IPersona
// ==========================

// Chef endpoints
app.MapPost("/chefs", (Chef chef) =>
{
    ValidadorDatos.ValidarEmail(chef.Email);
    ValidadorDatos.ValidarTelefono(chef.Telefono);
    chefs.Add(chef);
    return Results.Created($"/chefs/{chef.Id}", chef);
});

app.MapGet("/chefs", () => Results.Ok(chefs));

app.MapGet("/chefs/{id}", (int id) =>
{
    var chef = chefs.FirstOrDefault(c => c.Id == id);
    return chef is not null ? Results.Ok(chef) : Results.NotFound();
});

// Participante endpoints
app.MapPost("/participantes", (Participante participante) =>
{
    ValidadorDatos.ValidarEmail(participante.Email);
    ValidadorDatos.ValidarTelefono(participante.Telefono);
    participantes.Add(participante);
    return Results.Created($"/participantes/{participante.Id}", participante);
});

app.MapGet("/participantes", () => Results.Ok(participantes));

// Invitados especiales endpoints
app.MapPost("/invitados-especiales", (InvitadoEspecial invitado) =>
{
    ValidadorDatos.ValidarEmail(invitado.Email);
    ValidadorDatos.ValidarTelefono(invitado.Telefono);
    ValidadorDatos.ValidarTipoInvitado(invitado.TipoInvitado);
    invitadosEspeciales.Add(invitado);
    return Results.Created($"/invitados-especiales/{invitado.Id}", invitado);
});

app.MapGet("/invitados-especiales", () => Results.Ok(invitadosEspeciales));

// Endpoint unificado para todas las personas
app.MapGet("/personas", () =>
{
    var todasLasPersonas = chefs.Cast<IPersona>()
        .Concat(participantes.Cast<IPersona>())
        .Concat(invitadosEspeciales.Cast<IPersona>());
    return Results.Ok(todasLasPersonas);
});

// ==========================
// 🔹 ENDPOINTS EVENTOS - Trabajan con interfaz IEvento
// ==========================

// Eventos presenciales
app.MapPost("/eventos/presenciales", (EventoPresencial evento) =>
{
    eventos.Add(evento);
    return Results.Created($"/eventos/presenciales/{evento.Id}", evento);
});

// Eventos virtuales
app.MapPost("/eventos/virtuales", (EventoVirtual evento) =>
{
    eventos.Add(evento);
    return Results.Created($"/eventos/virtuales/{evento.Id}", evento);
});

// Endpoint unificado para todos los eventos
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

    reservas.RemoveAll(r => r.Evento.Id == id);
    eventos.Remove(evento);

    return Results.NoContent();
});

// ==========================
// 🔹 ENDPOINTS RESERVAS
// ==========================

app.MapPost("/reservas", (int participanteId, int eventoId) =>
{
    var participante = participantes.FirstOrDefault(p => p.Id == participanteId);
    var evento = eventos.FirstOrDefault(e => e.Id == eventoId);

    if (participante is null || evento is null)
        return Results.BadRequest("Participante o Evento no encontrado.");

    if (!evento.HayCupoDisponible())
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

// ==========================
// 🔹 ENDPOINTS INFORMES
// ==========================

app.MapGet("/informes", () =>
{
    var informesDisponibles = servicioInformes.ObtenerInformesDisponibles();
    return Results.Ok(new { informes = informesDisponibles });
});

app.MapGet("/informes/{clave}", (string clave) =>
{
    try
    {
        var informe = servicioInformes.GenerarInforme(clave);
        return Results.Text(informe, "text/plain");
    }
    catch (ErrorValidacionException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
});

app.MapGet("/informes/{clave}/info", (string clave) =>
{
    try
    {
        var (titulo, descripcion) = servicioInformes.ObtenerInformacionInforme(clave);
        return Results.Ok(new { titulo, descripcion });
    }
    catch (ErrorValidacionException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
});

app.MapGet("/informes/todos", () =>
{
    var todosLosInformes = servicioInformes.GenerarTodosLosInformes();
    return Results.Ok(todosLosInformes);
});

// ==========================
// 🔹 ENDPOINTS DE VALIDACIÓN
// ==========================

app.MapPost("/validar/email", (string email) =>
{
    try
    {
        ValidadorDatos.ValidarEmail(email);
        return Results.Ok(new { valido = true, mensaje = "Email válido" });
    }
    catch (ErrorValidacionException ex)
    {
        return Results.BadRequest(new { valido = false, error = ex.Message });
    }
});

app.MapPost("/validar/telefono", (string telefono) =>
{
    try
    {
        ValidadorDatos.ValidarTelefono(telefono);
        return Results.Ok(new { valido = true, mensaje = "Teléfono válido" });
    }
    catch (ErrorValidacionException ex)
    {
        return Results.BadRequest(new { valido = false, error = ex.Message });
    }
});

app.Run();