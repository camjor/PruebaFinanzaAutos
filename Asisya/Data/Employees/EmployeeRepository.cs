using System.Net;
using Microsoft.EntityFrameworkCore;
using Asisya.Models;
using Asisya.Middleware;

namespace Asisya.Data.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _context.Employees!
            .Include(e => e.Manager)
            .Include(e => e.Subordinates)
            .ToListAsync();
    }

    public async Task<Employee?> GetById(int id)
    {
        return await _context.Employees!
            .Include(e => e.Manager)
            .Include(e => e.Subordinates)
            .FirstOrDefaultAsync(e => e.EmployeeID == id);
    }

    public async Task Create(Employee employee)
    {
        if (employee is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { mensaje = "Los datos del empleado no son válidos" }
            );
        }

        await _context.Employees!.AddAsync(employee);
    }

    public async Task Delete(int id)
    {
        var employee = await GetById(id);

        if (employee is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { mensaje = $"No se encontró el empleado con id {id}" }
            );
        }

        _context.Employees!.Remove(employee);
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync()) >= 0;
    }
}
