using Application.Abstractions.Assignments;
using Application.Abstractions.Common;
using Domain.Entities;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Assignments;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly IApplicationDbContext _context;
    public AssignmentRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    //public async Task<Assignment> AddAssignment(Assignment assignment)
    //{
    //    var result = await _context.Assignments.AddAsync(assignment);
    //    await _context.SaveChangesAsync();

    //    return result.Entity;
    //}

    //public async Task<Assignment> DeleteAssignment(int id)
    //{
    //    var assignmentToDelete = await _context.Assignments.FirstOrDefaultAsync(ass => ass.Id == id);

    //    if (assignmentToDelete is null)
    //    {
    //        throw new NullReferenceException();
    //    }

    //    var result = _context.Assignments.Remove(assignmentToDelete);

    //    if (result is null)
    //    {
    //        throw new Exception();
    //    }

    //    await _context.SaveChangesAsync();

    //    return result.Entity;
    //}

    //public async Task<IEnumerable<Assignment>> GetAll()
    //{
    //    var assignments = await _context.Assignments
    //        .AsNoTracking()
    //        .ToListAsync();

    //    if (assignments is null)
    //    {
    //        assignments = new List<Assignment>();
    //    }

    //    return assignments;
    //}

    //public async Task<Assignment> GetById(int id)
    //{
    //    var assignment = await _context.Assignments
    //        .Include(a => a.Ticket)
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(ass => ass.Id == id);

    //    if (assignment is null)
    //    {
    //        throw new NullReferenceException();
    //    }

    //    return assignment;
    //}

    //public async Task<Assignment> UpdateAssignment(Assignment assignment)
    //{
    //    var updatedAssignment = _context.Assignments.Update(assignment);

    //    if (updatedAssignment is null)
    //    {
    //        throw new Exception();
    //    }

    //    await _context.SaveChangesAsync();

    //    return updatedAssignment.Entity;
    //}
}
