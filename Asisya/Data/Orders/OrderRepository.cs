using System.Net;
using Microsoft.EntityFrameworkCore;
using Asisya.Models;
using Asisya.Middleware;

namespace Asisya.Data.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _context.Orders!
            .Include(o => o.Customer)
            .Include(o => o.Employee)
            .Include(o => o.Shipper)
            .ToListAsync();
    }

    public async Task<Order?> GetById(int id)
    {
        return await _context.Orders!
            .Include(o => o.Customer)
            .Include(o => o.Employee)
            .Include(o => o.Shipper)
            .Include(o => o.OrderDetails!)
                .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(o => o.OrderID == id);
    }

    public async Task Create(Order order)
    {
        if (order is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { mensaje = "Los datos de la orden no son válidos" }
            );
        }

        await _context.Orders!.AddAsync(order);
    }

    public async Task Delete(int id)
    {
        var order = await GetById(id);

        if (order is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { mensaje = $"No se encontró la orden con id {id}" }
            );
        }

        _context.Orders!.Remove(order);
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync()) >= 0;
    }
}
