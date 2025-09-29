using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida
{
    public class EventoVirtual : EventoGastronomico
    {
        public string Plataforma { get; private set; }
        public string EnlaceAcceso { get; private set; }
        public bool RequiereSoftwareEspecial { get; private set; }
        public string SoftwareRequerido { get; private set; }
        public bool EsGrabado { get; private set; }
        public int DuracionMinutos { get; private set; }
        public decimal CostoTecnologia { get; private set; }

        public EventoVirtual(int id, string nombre, string descripcion, string tipo,
                        DateTime fechaInicio, DateTime fechaFin, int capacidadMaxima,
                        decimal precioBase, Chef organizador, string plataforma,
                        string enlaceAcceso, bool requiereSoftwareEspecial = false,
                        string softwareRequerido = "", bool esGrabado = false,
                        int duracionMinutos = 60, decimal costoTecnologia = 0)
            : base(id, nombre, descripcion, tipo, fechaInicio, fechaFin, capacidadMaxima, 
                precioBase, organizador)
        {
            if (string.IsNullOrWhiteSpace(plataforma))
                throw new ErrorValidacionException("La plataforma es requerida para eventos virtuales.");

            ValidadorDatos.ValidarPlataformaVirtual(plataforma);
            ValidadorDatos.ValidarDuracionEvento(duracionMinutos);

            Plataforma = plataforma;
            EnlaceAcceso = enlaceAcceso;
            RequiereSoftwareEspecial = requiereSoftwareEspecial;
            SoftwareRequerido = softwareRequerido;
            EsGrabado = esGrabado;
            DuracionMinutos = duracionMinutos;
            CostoTecnologia = costoTecnologia;
        }

        public override string ObtenerModalidad() => "Virtual";

        public override decimal CalcularPrecioFinal()
        {
            decimal descuentoVirtual = PrecioBase * 0.1m;
            return PrecioBase - descuentoVirtual + CostoTecnologia;
        }

        public override string ObtenerInformacionEvento()
        {
            string info = base.ObtenerInformacionEvento();
            info += $"\n💻 Plataforma: {Plataforma}";
            info += $"\n🔗 Enlace: {EnlaceAcceso}";
            info += $"\n⏱️ Duración: {DuracionMinutos} minutos";
            
            if (EsGrabado)
                info += " | 📹 Grabación disponible";
            if (RequiereSoftwareEspecial && !string.IsNullOrEmpty(SoftwareRequerido))
                info += $"\n⚙️ Software requerido: {SoftwareRequerido}";
                
            info += $"\n💰 Precio final: {CalcularPrecioFinal():C}";
            
            return info;
        }

        public override bool HayCupoDisponible()
        {
            if (CapacidadMaxima == 0)
                return true;
                
            return base.HayCupoDisponible();
        }

        public string ObtenerInstruccionesConexion()
        {
            return $"💻 Instrucciones para evento virtual:\n" +
                $"🖥️ Plataforma: {Plataforma}\n" +
                $"🔗 Enlace: {EnlaceAcceso}\n" +
                $"⏰ Hora: {FechaInicio:HH:mm} - Duración: {DuracionMinutos} minutos\n" +
                $"💡 Recomendación: Conectarse 10 minutos antes";
        }
    }
}