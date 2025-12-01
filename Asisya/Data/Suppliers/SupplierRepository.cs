using System.Net;
using Microsoft.EntityFrameworkCore;
using Asisya.Models;
using Asisya.Middleware;

namespace Asisya.Data.Suppliers;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Supplier>> GetAll()
    {
        return await _context.Suppliers!.ToListAsync();
    }

    public async Task<Supplier?> GetById(int id)
    {
        return await _context.Suppliers!
            .FirstOrDefaultAsync(s => s.SupplierID == id);
    }

    public async Task Create(Supplier supplier)
    {
        if (supplier is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { mensaje = "Los datos del proveedor no son válidos" }
            );
        }

        await _context.Suppliers!.AddAsync(supplier);
    }

    public async Task Delete(int id)
    {
        var supplier = await GetById(id);

        if (supplier is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { mensaje = $"No se encontró el proveedor con id {id}" }
            );
        }

        _context.Suppliers!.Remove(supplier);
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync()) >= 0;
    }
}
