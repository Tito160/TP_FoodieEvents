// Participante.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_EventoComida;

namespace Tp_EventoComida
{
    public class Participante : Persona
    {
        public string DocumentoIdentidad { get; private set; }
        public string RestriccionAlimentaria { get; private set; }

        public Participante(int id, string nombreCompleto, string email, string telefono, 
                        string documentoIdentidad, string restriccionAlimentaria = "")
            : base(id, nombreCompleto, email, telefono)
        {
            DocumentoIdentidad = documentoIdentidad;
            RestriccionAlimentaria = restriccionAlimentaria;
        }

        public override string PresentarInformacion()
        {
            string info = $"🎟️ Participante: {NombreCompleto} | Documento: {DocumentoIdentidad}";
            
            if (!string.IsNullOrEmpty(RestriccionAlimentaria))
            {
                info += $" | Restricciones: {RestriccionAlimentaria}";
            }
            
            return info;
        }

        public override void Registrar()
        {
            base.Registrar();
            if (!string.IsNullOrEmpty(RestriccionAlimentaria))
            {
                Console.WriteLine($"Se han registrado restricciones alimentarias: {RestriccionAlimentaria}");
            }
        }

        public override string ToString() => PresentarInformacion();
    }
}