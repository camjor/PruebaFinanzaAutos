using System.Net;
using Microsoft.EntityFrameworkCore;
using Asisya.Models;
using Asisya.Middleware;

namespace Asisya.Data.Shippers;

public class ShipperRepository : IShipperRepository
{
    private readonly AppDbContext _context;

    public ShipperRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shipper>> GetAll()
    {
        return await _context.Shippers!
            .Include(s => s.Orders)
            .ToListAsync();
    }

    public async Task<Shipper?> GetById(int id)
    {
        return await _context.Shippers!
            .Include(s => s.Orders)
            .FirstOrDefaultAsync(s => s.ShipperID == id);
    }

    public async Task Create(Shipper shipper)
    {
        if (shipper is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { mensaje = "Los datos del shipper no son válidos" }
            );
        }

        await _context.Shippers!.AddAsync(shipper);
    }

    public async Task Delete(int id)
    {
        var shipper = await GetById(id);

        if (shipper is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { mensaje = $"No se encontró el shipper con id {id}" }
            );
        }

        _context.Shippers!.Remove(shipper);
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync()) >= 0;
    }
}
