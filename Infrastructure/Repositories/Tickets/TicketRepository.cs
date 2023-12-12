using Application.Abstractions.Assignments;
using Application.Abstractions.Common;
using Domain.Entities;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Tickets;

public class TicketRepository : ITicketRepository
{
    private readonly IApplicationDbContext _context;

    public TicketRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    //public async Task<Ticket> AddTicket(Ticket ticket)
    //{
    //    var result = await _context.Tickets.AddAsync(ticket);
    //    await _context.SaveChangesAsync();

    //    return result.Entity;
    //}

    //public async Task<Ticket> DeleteTicket(int id)
    //{
    //    var ticketToDelete = await _context.Tickets.FirstOrDefaultAsync(ass => ass.Id == id);

    //    if (ticketToDelete is null)
    //    {
    //        throw new NullReferenceException();
    //    }

    //    var result = _context.Tickets.Remove(ticketToDelete);

    //    if (result is null)
    //    {
    //        throw new Exception();
    //    }

    //    await _context.SaveChangesAsync();

    //    return result.Entity;
    //}

    //public async Task<IEnumerable<Ticket>> GetAll()
    //{
    //    var tickets = await _context.Tickets
    //        .AsNoTracking()
    //        .ToListAsync();

    //    if (tickets is null)
    //    {
    //        tickets = new List<Ticket>();
    //    }

    //    return tickets;
    //}

    //public async Task<Ticket> GetById(int id)
    //{
    //    var ticket = await _context.Tickets
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(ass => ass.Id == id);

    //    if (ticket is null)
    //    {
    //        throw new NullReferenceException();
    //    }

    //    return ticket;
    //}

    //public async Task<Ticket> UpdateTicket(Ticket ticket)
    //{
    //    var updatedTicket = _context.Tickets.Update(ticket);

    //    if (updatedTicket is null)
    //    {
    //        throw new Exception();
    //    }

    //    await _context.SaveChangesAsync();

    //    return updatedTicket.Entity;
    //}
}
