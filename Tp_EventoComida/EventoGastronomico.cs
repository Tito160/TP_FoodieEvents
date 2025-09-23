using System;
using System.Collections.Generic;

namespace Tp_EventoComida
{
    public class EventoGastronomico
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string Tipo { get; private set; } // tipo como string
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public int CapacidadMaxima { get; private set; }
        public decimal PrecioPorEntrada { get; private set; }
        public string Ubicacion { get; private set; }
        public Chef Organizador { get; private set; }
        public List<Reserva> Reservas { get; private set; }

        // Constructor con validaciones
        public EventoGastronomico(int id, string nombre, string descripcion, string tipo, DateTime fechaInicio,
                                DateTime fechaFin, int capacidadMaxima, decimal precioPorEntrada,
                                string ubicacion, Chef organizador)
        {
            // Validaciones
            ValidadorDatos.ValidarTipoEvento(tipo);
            ValidadorDatos.ValidarFechasEvento(fechaInicio, fechaFin);
            ValidadorDatos.ValidarCapacidad(capacidadMaxima);
            ValidadorDatos.ValidarPrecio(precioPorEntrada);

            if (organizador == null)
                throw new ErrorValidacionException("El evento debe tener un chef organizador.");

            // Asignación de propiedades
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

        // Métodos de lógica
        public bool HayCupoDisponible() => Reservas.Count < CapacidadMaxima;

        public int LugaresDisponibles() => CapacidadMaxima - Reservas.Count;

        public void AgregarReserva(Reserva reserva)
        {
            if (!HayCupoDisponible())
                throw new ErrorValidacionException("No hay cupo disponible para este evento.");
            Reservas.Add(reserva);
        }

        public void CancelarReserva(Reserva reserva)
        {
            if (Reservas.Contains(reserva))
                Reservas.Remove(reserva);
        }

        // Al eliminar el evento, se eliminan todas las reservas (composición)
        public void EliminarEvento()
        {
            Reservas.Clear();
        }

        public override string ToString()
        {
            return $"Evento: {Nombre} ({Tipo}) | {FechaInicio:dd/MM/yyyy} - {FechaFin:dd/MM/yyyy} | Capacidad: {CapacidadMaxima}";
        }
    }
}
