using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class EventoGastronomico
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Descripcion { get; private set; }
    public string Tipo { get; private set; } // ahora es string
    public DateTime FechaInicio { get; private set; }
    public DateTime FechaFin { get; private set; }
    public int CapacidadMaxima { get; private set; }
    public decimal PrecioPorEntrada { get; private set; }
    public string Ubicacion { get; private set; }
    public Chef Organizador { get; private set; }
    public List<Reserva> Reservas { get; private set; }

    public EventoGastronomico(int id, string nombre, string descripcion, string tipo, DateTime fechaInicio,
                              DateTime fechaFin, int capacidadMaxima, decimal precioPorEntrada, string ubicacion, Chef organizador)
    {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
        Tipo = tipo;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        CapacidadMaxima = capacidadMaxima;
        PrecioPorEntrada = precioPorEntrada;
        Ubicacion = ubicacion;
        Organizador = organizador;
        Reservas = new List<Reserva>();
    }

    public bool HayCupoDisponible() => Reservas.Count < CapacidadMaxima;
    public int LugaresDisponibles() => CapacidadMaxima - Reservas.Count;

    public void AgregarReserva(Reserva reserva) => Reservas.Add(reserva);
    public void CancelarReserva(Reserva reserva) => Reservas.Remove(reserva);

    public void EliminarEvento() => Reservas.Clear(); // composición

    public override string ToString() => $"Evento: {Nombre} ({Tipo}) | {FechaInicio:dd/MM/yyyy}";
}

