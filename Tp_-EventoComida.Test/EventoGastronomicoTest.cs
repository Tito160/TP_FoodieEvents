using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Tp_EventoComida;

namespace Tp_EventoComida.Test
{
    public class EventoGastronomicoTest
    {
        private Chef CrearChef() =>
            new Chef(1, "Juan Perez", "Italiana", "Argentina", 10, "juan@mail.com", "123456789");

        [Fact]
        public void CrearEvento_Valido_DebeCrearCorrectamente()
        {
            var evento = new EventoGastronomico(1, "Cata de vinos", "Degustación", "Cata",
                DateTime.Today, DateTime.Today.AddDays(1), 50, 1000, "Buenos Aires", CrearChef());

            Assert.Equal("Cata de vinos", evento.Nombre);
            Assert.Equal(50, evento.CapacidadMaxima);
        }

        [Fact]
        public void CrearEvento_TipoInvalido_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() =>
                new EventoGastronomico(1, "Evento raro", "Descripcion", "Desconocido",
                    DateTime.Today, DateTime.Today.AddDays(1), 50, 1000, "Buenos Aires", CrearChef()));
        }

        [Fact]
        public void AgregarReserva_HayCupo_DebeAgregarCorrectamente()
        {
            var evento = new EventoGastronomico(1, "Cata de vinos", "Degustación", "Cata",
                DateTime.Today, DateTime.Today.AddDays(1), 1, 1000, "Buenos Aires", CrearChef());

            var participante = new Participante(1, "Maria Lopez", "maria@mail.com", "123456789", "DNI123");
            var reserva = new Reserva(1, participante, evento);

            evento.AgregarReserva(reserva);

            Assert.Single(evento.Reservas);
        }

        [Fact]
        public void AgregarReserva_SinCupo_DebeLanzarExcepcion()
        {
            var evento = new EventoGastronomico(1, "Cata de vinos", "Degustación", "Cata",
                DateTime.Today, DateTime.Today.AddDays(1), 1, 1000, "Buenos Aires", CrearChef());

            var participante = new Participante(1, "Maria Lopez", "maria@mail.com", "123456789", "DNI123");
            var reserva1 = new Reserva(1, participante, evento);
            evento.AgregarReserva(reserva1);

            var reserva2 = new Reserva(2, participante, evento);

            Assert.Throws<ErrorValidacionException>(() => evento.AgregarReserva(reserva2));
        }
    }
}
