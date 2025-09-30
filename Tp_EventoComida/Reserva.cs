using System;

using Tp_EventoComida;

namespace Tp_EventoComida
{
    public class Reserva
    {
        public int Id { get; private set; }
        public Participante Participante { get; private set; }
        public IEvento Evento { get; private set; }
        public DateTime FechaReserva { get; private set; }
        public bool Pagado { get; private set; }
        public string MetodoPago { get; private set; }
        public string Estado { get; private set; }

        public Reserva(int id, Participante participante, IEvento evento)
        {
            if (participante == null)
                throw new ErrorValidacionException("El participante es requerido para la reserva.");
            
            if (evento == null)
                throw new ErrorValidacionException("El evento es requerido para la reserva.");

            Id = id;
            Participante = participante;
            Evento = evento;
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

            Evento.AgregarReserva(this);
        }

        public void CancelarReserva()
        {
            Estado = "Cancelada";
        }

        public override string ToString()
        {
            return $"🎫 Reserva #{Id} - {Participante.NombreCompleto} | " +
                $"Evento: {Evento.Nombre} | Estado: {Estado} | Pagado: {Pagado}";
        }
    }
}