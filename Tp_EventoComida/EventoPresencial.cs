using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida
{
    public class EventoPresencial : EventoGastronomico
    {
        public string Ubicacion { get; private set; }
        public string Direccion { get; private set; }
        public string Ciudad { get; private set; }
        public bool TieneEstacionamiento { get; private set; }
        public bool TieneAccesibilidad { get; private set; }
        public decimal CostoLogistica { get; private set; }

        public EventoPresencial(int id, string nombre, string descripcion, string tipo,
                            DateTime fechaInicio, DateTime fechaFin, int capacidadMaxima,
                            decimal precioBase, Chef organizador, string ubicacion,
                            string direccion, string ciudad, bool tieneEstacionamiento = false,
                            bool tieneAccesibilidad = false, decimal costoLogistica = 0)
            : base(id, nombre, descripcion, tipo, fechaInicio, fechaFin, capacidadMaxima, 
                precioBase, organizador)
        {
            if (string.IsNullOrWhiteSpace(ubicacion))
                throw new ErrorValidacionException("La ubicación es requerida para eventos presenciales.");

            Ubicacion = ubicacion;
            Direccion = direccion;
            Ciudad = ciudad;
            TieneEstacionamiento = tieneEstacionamiento;
            TieneAccesibilidad = tieneAccesibilidad;
            CostoLogistica = costoLogistica;
        }

        public override string ObtenerModalidad() => "Presencial";

        public override decimal CalcularPrecioFinal()
        {
            return PrecioBase + CostoLogistica;
        }

        public override string ObtenerInformacionEvento()
        {
            string info = base.ObtenerInformacionEvento();
            info += $"\n📍 Ubicación: {Ubicacion} | {Direccion}, {Ciudad}";
            
            if (TieneEstacionamiento)
                info += " | 🅿️ Estacionamiento";
            if (TieneAccesibilidad)
                info += " | ♿ Accesible";
                
            info += $"\n💰 Precio final: {CalcularPrecioFinal():C} (Incluye logística: {CostoLogistica:C})";
            
            return info;
        }

        public string ObtenerInstruccionesAsistencia()
        {
            return $"📋 Instrucciones para evento presencial:\n" +
                $"🏢 Lugar: {Ubicacion}\n" +
                $"📫 Dirección: {Direccion}, {Ciudad}\n" +
                $"⏰ Hora: {FechaInicio:HH:mm} - {FechaFin:HH:mm}\n" +
                $"💡 Recomendación: Llegar 15 minutos antes";
        }
    }
}