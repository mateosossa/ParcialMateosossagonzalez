using System.Collections.Generic;
using System.Linq;

public class TicketRepository
{
    private readonly ConcertDbContext _dbContext;

    public TicketRepository(ConcertDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Ticket GetTicketById(int ticketId)
    {
        return _dbContext.Tickets.FirstOrDefault(t => t.TicketId == ticketId);
    }

    public void AddTicket(Ticket ticket)
    {
        _dbContext.Tickets.Add(ticket);
        _dbContext.SaveChanges();
    }

    public void UpdateTicket(Ticket ticket)
    {
        _dbContext.Tickets.Update(ticket);
        _dbContext.SaveChanges();
    }

    public List<Ticket> GetAllTickets()
    {
        return _dbContext.Tickets.ToList();
    }

}