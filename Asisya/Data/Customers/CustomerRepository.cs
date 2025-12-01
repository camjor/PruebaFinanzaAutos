using System.Net;
using Microsoft.EntityFrameworkCore;
using Asisya.Models;
using Asisya.Middleware;

namespace Asisya.Data.Customers;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _context.Customers!.ToListAsync();
    }

    public async Task<Customer?> GetById(string id)
    {
        return await _context.Customers!
            .FirstOrDefaultAsync(c => c.CustomerID == id);
    }

    public async Task Create(Customer customer)
    {
        if (customer is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { mensaje = "Los datos del cliente no son válidos" }
            );
        }

        await _context.Customers!.AddAsync(customer);
    }

    public async Task Delete(string id)
    {
        var customer = await GetById(id);

        if (customer is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { mensaje = $"No se encontró el cliente con id {id}" }
            );
        }

        _context.Customers!.Remove(customer);
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync()) >= 0;
    }
}
