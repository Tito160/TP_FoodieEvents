using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_EventoComida;
using Xunit;

namespace Tp__EventoComida.Test
{
    public class ValidadorDatosTest
    {
        [Fact]
        public void ValidarEmail_Invalido_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() => ValidadorDatos.ValidarEmail("invalido"));
        }

        [Fact]
        public void ValidarTelefono_Invalido_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() => ValidadorDatos.ValidarTelefono("abc"));
        }

        [Fact]
        public void ValidarFechasEvento_FinAntesQueInicio_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() => 
                ValidadorDatos.ValidarFechasEvento(DateTime.Today, DateTime.Today.AddDays(-1)));
        }

        [Fact]
        public void ValidarCapacidad_Invalida_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() => ValidadorDatos.ValidarCapacidad(0));
        }

        [Fact]
        public void ValidarPrecio_Negativo_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() => ValidadorDatos.ValidarPrecio(-1));
        }

        [Fact]
        public void ValidarTipoEvento_Invalido_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() => ValidadorDatos.ValidarTipoEvento("Festival"));
        }

        [Fact]
        public void ValidarMetodoPago_Invalido_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() => ValidadorDatos.ValidarMetodoPago("Criptomoneda"));
        }

        [Fact]
        public void ValidarEstadoReserva_Invalido_DebeLanzarExcepcion()
        {
            Assert.Throws<ErrorValidacionException>(() => ValidadorDatos.ValidarEstadoReserva("Pendiente"));
        }
    }
}
