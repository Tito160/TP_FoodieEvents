using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida
{
    /// <summary>
    /// Factory para creación flexible de diferentes tipos de eventos
    /// Permite crear eventos sin acoplar el código a implementaciones concretas
    /// </summary>
    public static class EventoFactory
    {
        /// <summary>
        /// Crea un evento según la modalidad especificada
        /// </summary>
        public static IEvento CrearEvento(string modalidad, int id, string nombre, string descripcion, 
                                        string tipo, DateTime fechaInicio, DateTime fechaFin, 
                                        int capacidadMaxima, decimal precioBase, Chef organizador, 
                                        object parametrosAdicionales)
        {
            return modalidad.ToLower() switch
            {
                "presencial" => CrearEventoPresencial(id, nombre, descripcion, tipo, fechaInicio, 
                                                    fechaFin, capacidadMaxima, precioBase, 
                                                    organizador, parametrosAdicionales),
                "virtual" => CrearEventoVirtual(id, nombre, descripcion, tipo, fechaInicio, 
                                            fechaFin, capacidadMaxima, precioBase, 
                                            organizador, parametrosAdicionales),
                _ => throw new ErrorValidacionException($"Modalidad no soportada: {modalidad}")
            };
        }

        /// <summary>
        /// Crea un evento presencial con parámetros específicos
        /// </summary>
        private static EventoPresencial CrearEventoPresencial(int id, string nombre, string descripcion,
                                                            string tipo, DateTime fechaInicio, DateTime fechaFin,
                                                            int capacidadMaxima, decimal precioBase, Chef organizador,
                                                            object parametros)
        {
            if (parametros is not Dictionary<string, object> presencialParams)
                throw new ErrorValidacionException("Parámetros inválidos para evento presencial");

            return new EventoPresencial(
                id, nombre, descripcion, tipo, fechaInicio, fechaFin, capacidadMaxima,
                precioBase, organizador,
                presencialParams["ubicacion"] as string ?? "",
                presencialParams["direccion"] as string ?? "",
                presencialParams["ciudad"] as string ?? "",
                presencialParams.ContainsKey("tieneEstacionamiento") && (bool)presencialParams["tieneEstacionamiento"],
                presicionalParams.ContainsKey("tieneAccesibilidad") && (bool)presencialParams["tieneAccesibilidad"],
                presencialParams.ContainsKey("costoLogistica") ? (decimal)presencialParams["costoLogistica"] : 0
            );
        }

        /// <summary>
        /// Crea un evento virtual con parámetros específicos
        /// </summary>
        private static EventoVirtual CrearEventoVirtual(int id, string nombre, string descripcion,
                                                    string tipo, DateTime fechaInicio, DateTime fechaFin,
                                                    int capacidadMaxima, decimal precioBase, Chef organizador,
                                                    object parametros)
        {
            if (parametros is not Dictionary<string, object> virtualParams)
                throw new ErrorValidacionException("Parámetros inválidos para evento virtual");

            return new EventoVirtual(
                id, nombre, descripcion, tipo, fechaInicio, fechaFin, capacidadMaxima,
                precioBase, organizador,
                virtualParams["plataforma"] as string ?? "",
                virtualParams["enlaceAcceso"] as string ?? "",
                virtualParams.ContainsKey("requiereSoftwareEspecial") && (bool)virtualParams["requiereSoftwareEspecial"],
                virtualParams["softwareRequerido"] as string ?? "",
                virtualParams.ContainsKey("esGrabado") && (bool)virtualParams["esGrabado"],
                virtualParams.ContainsKey("duracionMinutos") ? (int)virtualParams["duracionMinutos"] : 60,
                virtualParams.ContainsKey("costoTecnologia") ? (decimal)virtualParams["costoTecnologia"] : 0
            );
        }
    }
}