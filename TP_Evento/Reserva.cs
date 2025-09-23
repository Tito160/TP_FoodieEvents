using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class Reserva
{
    public int Id { get; private set; }
    public Participante Participante { get; private set; }
    public EventoGastronomico Evento { get; private set; }
    public DateTime FechaReserva { get; private set; }
    public bool Pagado { get; private set; }
    public string MetodoPago { get; private set; } // ahora string
    public string Estado { get; private set; } // ahora string

    public Reserva(int id, Participante participante, EventoGastronomico evento)
    {
        Id = id;
        Participante = participante;
        Evento = evento;
        FechaReserva = DateTime.Now;
        Pagado = false;
        Estado = "En espera";
    }

    public void ConfirmarPago(string metodoPago)
    {
        Pagado = true;
        MetodoPago = metodoPago;
        Estado = "Confirmada";
    }

    public void CancelarReserva() => Estado = "Cancelada";
    public void PonerEnEspera() => Estado = "En espera";

    public override string ToString() =>
        $"Reserva #{Id} - {Participante.NombreCompleto} | Estado: {Estado} | Pagado: {Pagado}";
}
