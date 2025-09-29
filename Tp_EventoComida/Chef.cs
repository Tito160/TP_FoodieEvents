using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida;

public class Chef : Persona
{
    public int Id { get; private set; }
    public string NombreCompleto { get; private set; }
    public string Especialidad { get; private set; }
    public string Nacionalidad { get; private set; }
    public int AniosExperiencia { get; private set; }
    public string Email { get; private set; }
    public string Telefono { get; private set; }

// Chef.cs
    /// Constructor específico para Chef que llama al constructor base
    public Chef(int id, string nombreCompleto, string especialidad, string nacionalidad, 
                int aniosExperiencia, string email, string telefono)
        : base(id, nombreCompleto, email, telefono) // ✅ Llama al constructor base
    {
        // 🏗️ INICIALIZACIÓN DE PROPIEDADES ESPECÍFICAS
        Especialidad = especialidad;
        Nacionalidad = nacionalidad;
        AniosExperiencia = aniosExperiencia;
    }

    // 🔹 IMPLEMENTACIÓN ESPECÍFICA DE COMPORTAMIENTOS

    /// Implementación específica para Chef del método abstracto
    /// Muestra información especializada del chef
    public override string PresentarInformacion()
    {
        return $"👨‍🍳 Chef: {NombreCompleto} | Especialidad: {Especialidad} | " +
            $"Experiencia: {AniosExperiencia} años | Nacionalidad: {Nacionalidad}";
    }

    /// Extiende el comportamiento base de registro con lógica específica de Chef
    public override void Registrar()
    {
        base.Registrar(); // ✅ Ejecuta el comportamiento base
        Console.WriteLine($"Chef especializado en {Especialidad} registrado exitosamente.");
    }

    /// Representación específica para Chef
    public override string ToString() => PresentarInformacion();
}