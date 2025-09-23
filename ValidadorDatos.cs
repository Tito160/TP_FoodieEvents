using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento
using System;
using System.Text.RegularExpressions;

namespace TP_Evento
{
    public static class ValidadorDatos
    {
        // Valida email con regex simple
        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || 
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ErrorValidacionException($"Email inválido: {email}");
            }
        }

        // Valida teléfono numérico y longitud lógica
        public static void ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono) || !Regex.IsMatch(telefono, @"^\d{7,15}$"))
            {
                throw new ErrorValidacionException($"Teléfono inválido: {telefono}");
            }
        }

        // Valida fechas de evento
        public static void ValidarFechasEvento(DateTime inicio, DateTime fin)
        {
            if (fin < inicio)
                throw new ErrorValidacionException("La fecha de fin no puede ser anterior a la fecha de inicio.");
        }

        // Valida que la capacidad máxima sea positiva
        public static void ValidarCapacidad(int capacidad)
        {
            if (capacidad <= 0)
                throw new ErrorValidacionException("La capacidad máxima debe ser mayor que 0.");
        }

        // Valida precio
        public static void ValidarPrecio(decimal precio)
        {
            if (precio < 0)
                throw new ErrorValidacionException("El precio no puede ser negativo.");
        }

        // Valida tipo de evento permitido
        public static void ValidarTipoEvento(string tipo)
        {
            string[] tiposValidos = { "Cata", "Feria", "Clase", "Cena Tematica" };
            if (Array.IndexOf(tiposValidos, tipo) == -1)
                throw new ErrorValidacionException($"Tipo de evento inválido: {tipo}");
        }

        // Valida método de pago
        public static void ValidarMetodoPago(string metodo)
        {
            string[] metodosValidos = { "Tarjeta", "Transferencia", "Efectivo" };
            if (Array.IndexOf(metodosValidos, metodo) == -1)
                throw new ErrorValidacionException($"Método de pago inválido: {metodo}");
        }

        // Valida estado de reserva
        public static void ValidarEstadoReserva(string estado)
        {
            string[] estadosValidos = { "Confirmada", "Cancelada", "En espera" };
            if (Array.IndexOf(estadosValidos, estado) == -1)
                throw new ErrorValidacionException($"Estado de reserva inválido: {estado}");
        }
    }
}
