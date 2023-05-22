using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly TicketService _ticketService;

    public TicketsController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet("{ticketId}")]
    public ActionResult<TicketDTO> GetTicket(int ticketId)
    {
        var ticket = _ticketService.GetTicket(ticketId);
        if (ticket == null)
            return NotFound();

        var ticketDTO = new TicketDTO
        {
            TicketId = ticket.TicketId,
            UseDate = ticket.UseDate,
            IsUsed = ticket.IsUsed,
            EntranceGate = ticket.EntranceGate
        };

        return ticketDTO;
    }

    [HttpPost]
    public ActionResult<TicketDTO> CreateTicket([FromBody] TicketDTO ticketDTO)
    {
        var ticket = new Ticket
        {
            TicketId = ticketDTO.TicketId,
            UseDate = null,
            IsUsed = false,
            EntranceGate = null
        };

        _ticketService.CreateTicket(ticket);

        return CreatedAtAction(nameof(GetTicket), new { ticketId = ticket.TicketId }, ticket);
    }

    [HttpPut("{ticketId}")]
    public IActionResult UpdateTicket(int ticketId, [FromBody] TicketDTO ticketDTO)
    {
        var ticket = _ticketService.GetTicket(ticketId);
        if (ticket == null)
            return NotFound();

        ticket.UseDate = ticketDTO.UseDate;
        ticket.IsUsed = true;
        ticket.EntranceGate = ticketDTO.EntranceGate;

        _ticketService.UpdateTicket(ticket);

        return NoContent();
    }

    [HttpGet("validate/{ticketId}")]
    public ActionResult<string> ValidateTicket(int ticketId)
    {
        var result = _ticketService.ValidateTicket(ticketId);
        return Ok(result);
    }

    [HttpPost("use/{ticketId}/{entranceGate}")]
    public IActionResult UseTicket(int ticketId, string entranceGate)
    {
        _ticketService.UseTicket(ticketId, entranceGate);
        return NoContent();
    }
}