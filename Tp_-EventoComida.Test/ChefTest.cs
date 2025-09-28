using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_EventoComida;
using Xunit;

namespace Tp__EventoComida.Test
{
    public class ChefTest
    {
        [Fact]
        public void CrearChef_Valido_DebeCrearCorrectamente()
        {
            var chef = new Chef(1, "Juan Perez", "Italiana", "Argentina", 10, "juan@mail.com", "123456789");

            Assert.Equal(1, chef.Id);
            Assert.Equal("Juan Perez", chef.NombreCompleto);
            Assert.Equal("Italiana", chef.Especialidad);
        }

        [Fact]
        public void ActualizarContacto_DebeCambiarEmailYTelefono()
        {
            var chef = new Chef(1, "Juan Perez", "Italiana", "Argentina", 10, "juan@mail.com", "123456789");
            chef.ActualizarContacto("nuevo@mail.com", "987654321");

            Assert.Equal("nuevo@mail.com", chef.Email);
            Assert.Equal("987654321", chef.Telefono);
        }
    }
}
