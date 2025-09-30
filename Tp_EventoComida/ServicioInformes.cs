using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_EventoComida.Interfaces; // ✅ AGREGA ESTE USING

namespace Tp_EventoComida
{
    public abstract class ServicioInformes : IGeneradorInformes
    {
        protected List<IEvento> EventoGastronomico { get; private set; }
        protected List<Persona> Personas { get; private set; }
        protected List<Reserva> Reservas { get; private set; }
        protected DateTime FechaGeneracion { get; private set; }

        protected ServicioInformes(List<IEvento> eventoGastronomico, List<Persona> personas, List<Reserva> reservas)
        {
            Eventos = eventoGastronomico ?? new List<IEvento>();
            Personas = personas ?? new List<Persona>();
            Reservas = reservas ?? new List<Reserva>();
            FechaGeneracion = DateTime.Now;
        }

        public virtual string GenerarInforme()
        {
            var informe = new StringBuilder();
            informe.AppendLine(GenerarCabecera());
            informe.AppendLine(GenerarCuerpo());
            informe.AppendLine(GenerarPie());
            return informe.ToString();
        }

        protected virtual string GenerarCabecera()
        {
            return $"=============================================\n" +
                $"📊 {ObtenerTitulo()}\n" +
                $"=============================================\n" +
                $"📋 Descripción: {ObtenerDescripcion()}\n" +
                $"📅 Fecha de generación: {FechaGeneracion:dd/MM/yyyy HH:mm}\n" +
                $"=============================================\n";
        }

        protected virtual string GenerarPie()
        {
            return $"=============================================\n" +
                $"📈 Total de eventos analizados: {Eventos.Count}\n" +
                $"👥 Total de personas en sistema: {Personas.Count}\n" +
                $"🎫 Total de reservas registradas: {Reservas.Count}\n" +
                $"=============================================";
        }

        protected Dictionary<IEvento, int> ObtenerAsistenciaPorEvento()
        {
            return Eventos.ToDictionary(
                evento => EventoGastronomico,
                evento => Reservas.Count(r => r.Evento.Id == evento.Id && r.Estado == "Confirmada")
            );
        }

        protected abstract string GenerarCuerpo();
        public abstract string ObtenerTitulo();
        public abstract string ObtenerDescripcion();
    }
}