using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// IEvento.cs
namespace Tp_EventoComida
{
    public interface IEvento
    {
        int Id { get; }
        string Nombre { get; }
        string Descripcion { get; }
        string Tipo { get; }
        DateTime FechaInicio { get; }
        DateTime FechaFin { get; }
        int CapacidadMaxima { get; }
        decimal PrecioBase { get; }
        Chef Organizador { get; }
        List<Reserva> Reservas { get; }
        
        bool HayCupoDisponible();
        int LugaresDisponibles();
        void AgregarReserva(Reserva reserva);
        void CancelarReserva(Reserva reserva);
        string ObtenerInformacionEvento();
        string ObtenerModalidad();
        decimal CalcularPrecioFinal();
    }
}