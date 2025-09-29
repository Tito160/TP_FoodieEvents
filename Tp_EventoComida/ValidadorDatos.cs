using System;
using System.Text.RegularExpressions;

namespace Tp_EventoComida
{
    public static class ValidadorDatos
    {
        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ErrorValidacionException($"Email inválido: {email}");
            }
        }

        public static void ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono) || !Regex.IsMatch(telefono, @"^\d{7,15}$"))
            {
                throw new ErrorValidacionException($"Teléfono inválido: {telefono}");
            }
        }

        public static void ValidarFechasEvento(DateTime inicio, DateTime fin)
        {
            if (fin < inicio)
                throw new ErrorValidacionException("La fecha de fin no puede ser anterior a la fecha de inicio.");
        }

        public static void ValidarCapacidad(int capacidad)
        {
            if (capacidad <= 0)
                throw new ErrorValidacionException("La capacidad máxima debe ser mayor que 0.");
        }

        public static void ValidarPrecio(decimal precio)
        {
            if (precio < 0)
                throw new ErrorValidacionException("El precio no puede ser negativo.");
        }

        public static void ValidarTipoEvento(string tipo)
        {
            string[] tiposValidos = { "Cata", "Feria", "Clase", "Cena Tematica" };
            if (Array.IndexOf(tiposValidos, tipo) == -1)
                throw new ErrorValidacionException($"Tipo de evento inválido: {tipo}");
        }

        public static void ValidarMetodoPago(string metodo)
        {
            string[] metodosValidos = { "Tarjeta", "Transferencia", "Efectivo" };
            if (Array.IndexOf(metodosValidos, metodo) == -1)
                throw new ErrorValidacionException($"Método de pago inválido: {metodo}");
        }

        public static void ValidarEstadoReserva(string estado)
        {
            string[] estadosValidos = { "Confirmada", "Cancelada", "En espera" };
            if (Array.IndexOf(estadosValidos, estado) == -1)
                throw new ErrorValidacionException($"Estado de reserva inválido: {estado}");
        }

        public static void ValidarTipoInvitado(string tipo)
        {
            string[] tiposValidos = { "Crítico", "Influencer", "Figura Reconocida" };
            if (Array.IndexOf(tiposValidos, tipo) == -1)
                throw new ErrorValidacionException($"Tipo de invitado inválido: {tipo}");
        }

        public static void ValidarPlataformaVirtual(string plataforma)
        {
            string[] plataformasValidas = { "Zoom", "Teams", "YouTube", "Google Meet", "Webex", "Otra" };
            if (Array.IndexOf(plataformasValidas, plataforma) == -1)
                throw new ErrorValidacionException($"Plataforma virtual no soportada: {plataforma}");
        }

        public static void ValidarCapacidadVirtual(int capacidad)
        {
            if (capacidad < 0)
                throw new ErrorValidacionException("La capacidad no puede ser negativa.");
        }

        public static void ValidarDuracionEvento(int duracionMinutos)
        {
            if (duracionMinutos <= 0)
                throw new ErrorValidacionException("La duración del evento debe ser mayor a 0 minutos.");
        }
    }
}