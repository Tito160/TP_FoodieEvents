using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida
{
    public class InvitadoEspecial : Persona
    {
        public string TipoInvitado { get; private set; }
        public string Especialidad { get; private set; }
        public int Seguidores { get; private set; }

        public InvitadoEspecial(int id, string nombreCompleto, string email, string telefono,
                            string tipoInvitado, string especialidad = "", int seguidores = 0)
            : base(id, nombreCompleto, email, telefono)
        {
            ValidarTipoInvitado(tipoInvitado);
            
            TipoInvitado = tipoInvitado;
            Especialidad = especialidad;
            Seguidores = seguidores;
        }
        private void ValidarTipoInvitado(string tipo)
        {
            string[] tiposValidos = { "Crítico", "Influencer", "Figura Reconocida" };
            if (Array.IndexOf(tiposValidos, tipo) == -1)
                throw new ErrorValidacionException($"Tipo de invitado inválido: {tipo}");
        }
        public override string PresentarInformacion()
        {
            string info = $"⭐ {TipoInvitado}: {NombreCompleto}";
            
            if (!string.IsNullOrEmpty(Especialidad))
            {
                info += $" | Especialidad: {Especialidad}";
            }
            
            if (Seguidores > 0)
            {
                info += $" | Seguidores: {Seguidores:N0}";
            }
            
            info += " | Acceso: Gratuito";
            return info;
        }

        public decimal CalcularCostoAcceso(EventoBase evento)
        {
            return 0;
        }

        public override void Registrar()
        {
            base.Registrar();
            Console.WriteLine($"Invitado especial ({TipoInvitado}) registrado con acceso gratuito.");
        }

        public override string ToString() => PresentarInformacion();
    }
}