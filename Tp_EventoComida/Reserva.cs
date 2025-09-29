using System;
using Tp_EventoComida;

namespace Tp_EventoComida
{
    public class Reserva
    {
        public int Id { get; private set; }
        public Participante Participante { get; private set; }
        public IEvento EventoGastronomico { get; private set; } // ✅ Nombre consistente
        public DateTime FechaReserva { get; private set; }
        public bool Pagado { get; private set; }
        public string MetodoPago { get; private set; }
        public string Estado { get; private set; }

        /// <summary>
        /// Constructor con validaciones
        /// </summary>
        public Reserva(int id, Participante participante, IEvento eventoGastronomico)
        {
            // ✅ VALIDACIONES DE NULL
            if (participante == null)
                throw new ErrorValidacionException("El participante es requerido para la reserva.");
            
            if (evento == null)
                throw new ErrorValidacionException("El evento es requerido para la reserva.");

            // ✅ ASIGNACIONES CORRECTAS
            Id = id;
            Participante = participante;
            Evento = evento; // ✅ Mismo nombre que la propiedad
            FechaReserva = DateTime.Now;
            Pagado = false;
            Estado = "En espera";
        }

        public void ConfirmarPago(string metodoPago)
        {
            ValidadorDatos.ValidarMetodoPago(metodoPago);
            
            Pagado = true;
            MetodoPago = metodoPago;
            Estado = "Confirmada";

            // ✅ Agregar reserva al evento
            Evento.AgregarReserva(this);
        }

        public void CancelarReserva()
        {
            Estado = "Cancelada";
        }

        public override string ToString()
        {
            return $"🎫 Reserva #{Id} - {Participante.NombreCompleto} | " +
                   $"Evento: {EventoGastronomico.Nombre} | Estado: {Estado} | Pagado: {Pagado}"; // ✅ Evento (no EventoGastronomico)
        }
    }
}