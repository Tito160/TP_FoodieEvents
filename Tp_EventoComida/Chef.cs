using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida
{
    public class Chef : Persona
    {
        public string Especialidad { get; private set; }
        public string Nacionalidad { get; private set; }
        public int AniosExperiencia { get; private set; }

        public Chef(int id, string nombreCompleto, string especialidad, string nacionalidad, 
                    int aniosExperiencia, string email, string telefono)
            : base(id, nombreCompleto, email, telefono)
        {
            Especialidad = especialidad;
            Nacionalidad = nacionalidad;
            AniosExperiencia = aniosExperiencia;
        }

        public override string PresentarInformacion()
        {
            return $"👨‍🍳 Chef: {NombreCompleto} | Especialidad: {Especialidad} | " +
                $"Experiencia: {AniosExperiencia} años | Nacionalidad: {Nacionalidad}";
        }

        public override void Registrar()
        {
            base.Registrar();
            Console.WriteLine($"Chef especializado en {Especialidad} registrado exitosamente.");
        }

        public override string ToString() => PresentarInformacion();
    }
}