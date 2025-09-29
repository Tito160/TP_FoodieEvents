using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida
{
    /// Clase InvitadoEspecial que hereda de Persona y especializa el comportamiento 
    /// para invitados especiales (críticos, influencers, figuras reconocidas)
    /// </summary>
    public class InvitadoEspecial : Persona
    {
        // 🔹 PROPIEDADES ESPECÍFICAS DE INVITADO ESPECIAL
        public string TipoInvitado { get; private set; } // "Crítico", "Influencer", "Figura Reconocida"
        public string Especialidad { get; private set; }
        public int Seguidores { get; private set; } // Específico para influencers

        /// <summary>
        /// Constructor específico para InvitadoEspecial
        /// </summary>
        public InvitadoEspecial(int id, string nombreCompleto, string email, string telefono,
                            string tipoInvitado, string especialidad = "", int seguidores = 0)
            : base(id, nombreCompleto, email, telefono) // ✅ Llama al constructor base
        {
            // ✅ VALIDACIÓN ESPECÍFICA
            ValidarTipoInvitado(tipoInvitado);
            
            // 🏗️ INICIALIZACIÓN DE PROPIEDADES ESPECÍFICAS
            TipoInvitado = tipoInvitado;
            Especialidad = especialidad;
            Seguidores = seguidores;
        }

        /// <summary>
        /// Valida que el tipo de invitado sea uno de los permitidos
        /// </summary>
        private void ValidarTipoInvitado(string tipo)
        {
            string[] tiposValidos = { "Crítico", "Influencer", "Figura Reconocida" };
            if (Array.IndexOf(tiposValidos, tipo) == -1)
                throw new ErrorValidacionException($"Tipo de invitado inválido: {tipo}");
        }

        // 🔹 IMPLEMENTACIÓN ESPECÍFICA DE COMPORTAMIENTOS

        /// <summary>
        /// Implementación específica para InvitadoEspecial del método abstracto
        /// Muestra información especializada del invitado
        /// </summary>
        public override string PresentarInformacion()
        {
            string info = $"⭐ {TipoInvitado}: {NombreCompleto}";
            
            // 📋 Información adicional según el tipo de invitado
            if (!string.IsNullOrEmpty(Especialidad))
            {
                info += $" | Especialidad: {Especialidad}";
            }
            
            // 👥 Información específica para influencers
            if (Seguidores > 0)
            {
                info += $" | Seguidores: {Seguidores:N0}";
            }
            
            // 💰 Información sobre acceso gratuito
            info += " | Acceso: Gratuito";
            return info;
        }

        // 🔹 COMPORTAMIENTOS ESPECÍFICOS DE INVITADO ESPECIAL

        /// <summary>
        /// Calcula el costo de acceso para invitados especiales (siempre gratuito)
        /// </summary>
        public decimal CalcularCostoAcceso(EventoGastronomico evento)
        {
            // 🆓 Invitados especiales no pagan - comportamiento específico
            return 0;
        }

        /// <summary>
        /// Extiende el comportamiento base de registro con lógica específica de InvitadoEspecial
        /// </summary>
        public override void Registrar()
        {
            base.Registrar(); // ✅ Ejecuta el comportamiento base
            Console.WriteLine($"Invitado especial ({TipoInvitado}) registrado con acceso gratuito.");
        }

        /// <summary>
        /// Representación específica para InvitadoEspecial
        /// </summary>
        public override string ToString() => PresentarInformacion();
    }
}