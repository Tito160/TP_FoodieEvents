// Participante.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_EventoComida;

namespace Tp_EventoComida;

/// Clase Participante que hereda de Persona y especializa el comportamiento para participantes
public class Participante : Persona
{
    // 🔹 PROPIEDADES ESPECÍFICAS DE PARTICIPANTE
    public string DocumentoIdentidad { get; private set; }
    public string RestriccionAlimentaria { get; private set; }

    /// <summary>
    /// Constructor específico para Participante
    /// </summary>
    public Participante(int id, string nombreCompleto, string email, string telefono, 
                    string documentoIdentidad, string restriccionAlimentaria = "")
        : base(id, nombreCompleto, email, telefono) // ✅ Llama al constructor base
    {
        // 🏗️ INICIALIZACIÓN DE PROPIEDADES ESPECÍFICAS
        DocumentoIdentidad = documentoIdentidad;
        RestriccionAlimentaria = restriccionAlimentaria;
    }

    // 🔹 IMPLEMENTACIÓN ESPECÍFICA DE COMPORTAMIENTOS

    /// <summary>
    /// Implementación específica para Participante del método abstracto
    /// Muestra información especializada del participante
    /// </summary>
    public override string PresentarInformacion()
    {
        string info = $"🎟️ Participante: {NombreCompleto} | Documento: {DocumentoIdentidad}";
        
        // 📋 Información adicional sobre restricciones alimentarias
        if (!string.IsNullOrEmpty(RestriccionAlimentaria))
        {
            info += $" | Restricciones: {RestriccionAlimentaria}";
        }
        
        return info;
    }

    /// <summary>
    /// Extiende el comportamiento base de registro con lógica específica de Participante
    /// </summary>
    public override void Registrar()
    {
        base.Registrar(); // ✅ Ejecuta el comportamiento base
        
        // 🚨 Información importante sobre restricciones alimentarias
        if (!string.IsNullOrEmpty(RestriccionAlimentaria))
        {
            Console.WriteLine($"Se han registrado restricciones alimentarias: {RestriccionAlimentaria}");
        }
    }

    /// <summary>
    /// Representación específica para Participante
    /// </summary>
    public override string ToString() => PresentarInformacion();
}