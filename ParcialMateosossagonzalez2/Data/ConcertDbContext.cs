using System.Collections.Generic;
using System.Net.Sockets;




using Microsoft.EntityFrameworkCore;

public class ConcertDbContext : DbContext
{
    public DbSet<Ticket> Tickets { get; set; }

    public ConcertDbContext(DbContextOptions<ConcertDbContext> options) : base(options)
    {
    }
}