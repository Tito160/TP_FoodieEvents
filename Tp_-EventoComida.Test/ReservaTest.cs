using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_EventoComida;
using Xunit;

namespace Tp__EventoComida.Test
{
    public class ReservaTest
    {
        private Chef CrearChef() =>
            new Chef(1, "Juan Perez", "Italiana", "Argentina", 10, "juan@mail.com", "123456789");

        private EventoGastronomico CrearEvento() =>
            new EventoGastronomico(1, "Cata de vinos", "Degustación", "Cata",
                DateTime.Today, DateTime.Today.AddDays(1), 50, 1000, "Buenos Aires", CrearChef());

        private Participante CrearParticipante() =>
            new Participante(1, "Maria Lopez", "maria@mail.com", "123456789", "DNI123");

        [Fact]
        public void CrearReserva_Valida_DebeCrearEnEspera()
        {
            var reserva = new Reserva(1, CrearParticipante(), CrearEvento());

            Assert.Equal("En espera", reserva.Estado);
            Assert.False(reserva.Pagado);
        }

        [Fact]
        public void ConfirmarPago_Valido_DebeCambiarEstadoAPagado()
        {
            var reserva = new Reserva(1, CrearParticipante(), CrearEvento());
            reserva.ConfirmarPago("Tarjeta");

            Assert.True(reserva.Pagado);
            Assert.Equal("Confirmada", reserva.Estado);
        }

        [Fact]
        public void ConfirmarPago_MetodoInvalido_DebeLanzarExcepcion()
        {
            var reserva = new Reserva(1, CrearParticipante(), CrearEvento());

            Assert.Throws<ErrorValidacionException>(() => reserva.ConfirmarPago("Bitcoin"));
        }

        [Fact]
        public void CancelarReserva_DebeCambiarEstadoACancelada()
        {
            var reserva = new Reserva(1, CrearParticipante(), CrearEvento());
            reserva.CancelarReserva();

            Assert.Equal("Cancelada", reserva.Estado);
        }
    }
}
