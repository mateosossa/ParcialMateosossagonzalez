public class TicketService
{
    private readonly TicketRepository _ticketRepository;

    public TicketService(TicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public Ticket GetTicket(int ticketId)
    {
        return _ticketRepository.GetTicketById(ticketId);
    }

    public void CreateTicket(Ticket ticket)
    {
        _ticketRepository.AddTicket(ticket);
    }

    public void UpdateTicket(Ticket ticket)
    {
        _ticketRepository.UpdateTicket(ticket);
    }

    public bool IsTicketUsed(int ticketId)
    {
        var ticket = _ticketRepository.GetTicketById(ticketId);
        return ticket != null && ticket.IsUsed;
    }

    public string ValidateTicket(int ticketId)
    {
        var ticket = _ticketRepository.GetTicketById(ticketId);
        if (ticket == null)
            return $"Boleta no válida. No se encontró boleta con ID {ticketId}";

        if (ticket.IsUsed)
            return $"Boleta con ID {ticketId} ya ha sido utilizada el {ticket.UseDate} por la portería {ticket.EntranceGate}";

        return $"Boleta con ID {ticketId} es válida y puede ingresar al concierto";
    }

    public void UseTicket(int ticketId, string entranceGate)
    {
        var ticket = _ticketRepository.GetTicketById(ticketId);
        if (ticket != null && !ticket.IsUsed)
        {
            ticket.UseDate = DateHelper.GetCurrentDate();
            ticket.IsUsed = true;
            ticket.EntranceGate = entranceGate;

            _ticketRepository.UpdateTicket(ticket);
        }
    }
}
