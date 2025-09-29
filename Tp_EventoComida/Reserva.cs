using System;
using Tp_EventoComida;

namespace Tp_EventoComida
{
    public class Reserva
    {
        public int Id { get; private set; }
        public Participante Participante { get; private set; }
        public IEvento EventoGastronomico { get; private set; } // ✅ Cambiar de EventoGastronomico a IEvento
        public DateTime FechaReserva { get; private set; }
        public bool Pagado { get; private set; }
        public string MetodoPago { get; private set; }
        public string Estado { get; private set; }

        /// <summary>
        /// Constructor actualizado para trabajar con cualquier tipo de evento
        /// </summary>
        public Reserva(int id, Participante participante, IEvento evento) // ✅ Cambiar parámetro
        {
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

            // ✅ Agregar reserva al evento (funciona con cualquier implementación de IEvento)
            Evento.AgregarReserva(this);
        }

        // Resto de métodos permanecen igual...
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