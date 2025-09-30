using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida.Interfaces
{
    public interface IGeneradorInformes
    {
        string GenerarInforme();
        string ObtenerTitulo();
        string ObtenerDescripcion();
    }
}