using System;

namespace Tp_EventoComida
{
    public class Reserva
    {
        public int Id { get; private set; }
        public Participante Participante { get; private set; }
        public EventoGastronomico Evento { get; private set; }
        public DateTime FechaReserva { get; private set; }
        public bool Pagado { get; private set; }
        public string MetodoPago { get; private set; } // string
        public string Estado { get; private set; } // string

        // Constructor
        public Reserva(int id, Participante participante, EventoGastronomico evento)
        {
            if (participante == null)
                throw new ErrorValidacionException("Debe indicar un participante válido.");

            if (evento == null)
                throw new ErrorValidacionException("Debe indicar un evento válido.");

            Id = id;
            Participante = participante;
            Evento = evento;
            FechaReserva = DateTime.Now;
            Pagado = false;
            MetodoPago = string.Empty;
            Estado = "En espera";
        }

        // Confirmar pago con validación
        public void ConfirmarPago(string metodoPago)
        {
            ValidadorDatos.ValidarMetodoPago(metodoPago);

            Pagado = true;
            MetodoPago = metodoPago;
            Estado = "Confirmada";
        }

        // Cancelar reserva
        public void CancelarReserva()
        {
            Estado = "Cancelada";
        }

        // Poner en espera
        public void PonerEnEspera()
        {
            Estado = "En espera";
        }

        // Resumen de la reserva
        public override string ToString()
        {
            return $"Reserva #{Id} - {Participante.NombreCompleto} | Estado: {Estado} | Pagado: {Pagado}";
        }
    }
}
