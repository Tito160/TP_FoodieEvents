using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_EventoComida.Interfaces; // ✅ AGREGA ESTE USING

namespace Tp_EventoComida
{
    public class ServicioInformes : IGeneradorInformes
    {
        public List<IEvento> Evento { get; private set; }
        public List<Persona> Personas { get; private set; }
        public List<Reserva> Reservas { get; private set; }
        public DateTime FechaGeneracion { get; private set; }

        public ServicioInformes(List<IEvento> evento, List<Persona> personas, List<Reserva> reservas)
        {
            Evento = evento ?? new List<IEvento>();
            Personas = personas ?? new List<Persona>();
            Reservas = reservas ?? new List<Reserva>();
            FechaGeneracion = DateTime.Now;
        }

        public string GenerarInforme()
        {
            var informe = new StringBuilder();
            informe.AppendLine(GenerarCabecera());
            informe.AppendLine(GenerarCuerpo());
            informe.AppendLine(GenerarPie());
            return informe.ToString();
        }

        public string GenerarCabecera()
        {
            return $"=============================================\n" +
                $"📊 {ObtenerTitulo()}\n" +
                $"=============================================\n" +
                $"📋 Descripción: {ObtenerDescripcion()}\n" +
                $"📅 Fecha de generación: {FechaGeneracion:dd/MM/yyyy HH:mm}\n" +
                $"=============================================\n";
        }

        public string GenerarPie()
        {
            return $"=============================================\n" +
                $"📈 Total de eventos analizados: {Evento.Count}\n" +
                $"👥 Total de personas en sistema: {Personas.Count}\n" +
                $"🎫 Total de reservas registradas: {Reservas.Count}\n" +
                $"=============================================";
        }

        /*
                public Dictionary<IEvento, int> ObtenerAsistenciaPorEvento()
                {
                    return Evento.ToDictionary(
                        Evento => Evento,
                        EventoGastronomico => Reservas.Count(r => r.Evento.Id == Evento. && r.Estado == "Confirmada")
                    );
                } */

        public string GenerarCuerpo()
        {
            throw new Exception("no implementado");
        }
        public string ObtenerTitulo()
        {
            throw new Exception("no implementado");
        }
        public string ObtenerDescripcion()
        {
                        throw new Exception("no implementado");
        }

        internal (object titulo, object descripcion) ObtenerInformacionInforme(string clave)
        {
            throw new NotImplementedException();
        }

        internal object? GenerarTodosLosInformes()
        {
            throw new NotImplementedException();
        }

        internal string? GenerarInforme(string clave)
        {
            throw new NotImplementedException();
        }
    }
}