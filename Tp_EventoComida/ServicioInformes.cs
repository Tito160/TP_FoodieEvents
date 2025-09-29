using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida
{
    public class ServicioInformes
    {
        private readonly List<EventoBase> _eventos;
        private readonly List<Persona> _personas;
        private readonly List<Reserva> _reservas;
        private readonly Dictionary<string, IGeneradorInformes> _generadores;

        public ServicioInformes(List<EventoBase> eventos, List<Persona> personas, List<Reserva> reservas)
        {
            _eventos = eventos ?? throw new ArgumentNullException(nameof(eventos));
            _personas = personas ?? throw new ArgumentNullException(nameof(personas));
            _reservas = reservas ?? throw new ArgumentNullException(nameof(reservas));
            
            _generadores = new Dictionary<string, IGeneradorInformes>();
            RegistrarGeneradoresPorDefecto();
        }

        private void RegistrarGeneradoresPorDefecto()
        {
            RegistrarGenerador("asistencia", new InformeEventosMayorAsistencia(_eventos, _personas, _reservas));
            RegistrarGenerador("trayectoria", new InformeOrganizadoresTrayectoria(_eventos, _personas, _reservas));
            RegistrarGenerador("participacion", new InformeParticipacionMultiple(_eventos, _personas, _reservas));
        }

        public void RegistrarGenerador(string clave, IGeneradorInformes generador)
        {
            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("La clave no puede estar vacía", nameof(clave));
                
            _generadores[clave.ToLower()] = generador;
        }

        public string GenerarInforme(string clave)
        {
            if (_generadores.TryGetValue(clave.ToLower(), out var generador))
            {
                return generador.GenerarInforme();
            }
            
            throw new ErrorValidacionException($"No se encontró un generador de informes con la clave: {clave}");
        }

        public List<string> ObtenerInformesDisponibles()
        {
            return _generadores.Keys.ToList();
        }

        public (string titulo, string descripcion) ObtenerInformacionInforme(string clave)
        {
            if (_generadores.TryGetValue(clave.ToLower(), out var generador))
            {
                return (generador.ObtenerTitulo(), generador.ObtenerDescripcion());
            }
            
            throw new ErrorValidacionException($"No se encontró un generador de informes con la clave: {clave}");
        }

        public Dictionary<string, string> GenerarTodosLosInformes()
        {
            var resultados = new Dictionary<string, string>();
            
            foreach (var (clave, generador) in _generadores)
            {
                resultados[clave] = generador.GenerarInforme();
            }
            
            return resultados;
        }
    }
}