using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_EventoComida;
using Xunit;

namespace Tp_EventoComida.Test
{
    public class ParticipanteTest
    {
        [Fact]
        public void CrearParticipante_Valido_DebeCrearCorrectamente()
        {
            var participante = new Participante(1, "Maria Lopez", "maria@mail.com", "123456789", "DNI123");

            Assert.Equal("Maria Lopez", participante.NombreCompleto);
            Assert.Equal("maria@mail.com", participante.Email);
        }

        [Fact]
        public void CrearParticipante_EmailInvalido_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() =>
                new Participante(1, "Maria Lopez", "email_invalido", "123456789", "DNI123"));
        }

        [Fact]
        public void ActualizarContacto_EmailValido_DebeActualizarCorrectamente()
        {
            var participante = new Participante(1, "Maria Lopez", "maria@mail.com", "123456789", "DNI123");
            participante.ActualizarContacto("nuevo@mail.com", "987654321");

            Assert.Equal("nuevo@mail.com", participante.Email);
            Assert.Equal("987654321", participante.Telefono);
        }
    }
}
