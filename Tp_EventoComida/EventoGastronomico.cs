using System;
using System.Collections.Generic;
using Tp_EventoComida;

namespace Tp_EventoComida
{
    public abstract class EventoGastronomico : IEvento
    {
        public int Id { get; protected set; }
        public string Nombre { get; protected set; }
        public string Descripcion { get; protected set; }
        public string Tipo { get; protected set; }
        public DateTime FechaInicio { get; protected set; }
        public DateTime FechaFin { get; protected set; }
        public int CapacidadMaxima { get; protected set; }
        public decimal PrecioBase { get; protected set; }
        public Chef Organizador { get; protected set; }
        public List<Reserva> Reservas { get; protected set; }

        protected EventoGastronomico(int id, string nombre, string descripcion, string tipo, 
                        DateTime fechaInicio, DateTime fechaFin, int capacidadMaxima, 
                        decimal precioBase, Chef organizador)
        {
            ValidadorDatos.ValidarTipoEvento(tipo);
            ValidadorDatos.ValidarFechasEvento(fechaInicio, fechaFin);
            ValidadorDatos.ValidarCapacidad(capacidadMaxima);
            ValidadorDatos.ValidarPrecio(precioBase);

            if (organizador == null)
                throw new ErrorValidacionException("El evento debe tener un chef organizador.");

            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Tipo = tipo;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            CapacidadMaxima = capacidadMaxima;
            PrecioBase = precioBase;
            Organizador = organizador;
            Reservas = new List<Reserva>();
        }

        public virtual bool HayCupoDisponible() => Reservas.Count < CapacidadMaxima;

        public virtual int LugaresDisponibles() => CapacidadMaxima - Reservas.Count;

        public virtual void AgregarReserva(Reserva reserva)
        {
            if (!HayCupoDisponible())
                throw new ErrorValidacionException("No hay cupo disponible para este evento.");
            Reservas.Add(reserva);
        }

        public virtual void CancelarReserva(Reserva reserva)
        {
            if (Reservas.Contains(reserva))
                Reservas.Remove(reserva);
        }

        public virtual string ObtenerInformacionEvento()
        {
            return $"📅 Evento: {Nombre} ({Tipo}) | " +
                $"{FechaInicio:dd/MM/yyyy} - {FechaFin:dd/MM/yyyy} | " +
                $"👥 Capacidad: {CapacidadMaxima} | 🏷️ Modalidad: {ObtenerModalidad()}";
        }

        public abstract string ObtenerModalidad();
        public abstract decimal CalcularPrecioFinal();

        public override string ToString() => ObtenerInformacionEvento();
    }
}