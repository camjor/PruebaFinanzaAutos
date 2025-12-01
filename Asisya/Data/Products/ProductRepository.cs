using System.Net;
using Microsoft.EntityFrameworkCore;
using Asisya.Models;
using Asisya.Middleware;

namespace Asisya.Data.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products!
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .ToListAsync();
    }

    public async Task<Product?> GetById(int id)
    {
        return await _context.Products!
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.ProductID == id);
    }

    public async Task Create(Product product)
    {
        if (product is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { mensaje = "Los datos del producto no son válidos" }
            );
        }

        await _context.Products!.AddAsync(product);
    }

    // -----------------------------
    // CARGA MASIVA
    // -----------------------------
    public async Task BulkCreate(int count, int? categoryId, int? supplierId)
    {
        if (count <= 0)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { mensaje = "El parámetro count debe ser mayor que cero" }
            );
        }

        const int batchSize = 5000;
        var list = new List<Product>();

        for (int i = 0; i < count; i++)
        {
            list.Add(new Product
            {
                ProductName = $"Producto_{Guid.NewGuid()}",
                UnitPrice = Random.Shared.Next(1, 500),
                UnitsInStock = (short)Random.Shared.Next(0, 100),
                CategoryID = categoryId,
                SupplierID = supplierId
            });

            if (list.Count >= batchSize)
            {
                await _context.Products!.AddRangeAsync(list);
                await _context.SaveChangesAsync();
                list.Clear();
            }
        }

        if (list.Count > 0)
        {
            await _context.Products!.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var product = await GetById(id);

        if (product is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { mensaje = $"No se encontró el producto con id {id}" }
            );
        }

        _context.Products!.Remove(product);
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync()) >= 0;
    }
}
