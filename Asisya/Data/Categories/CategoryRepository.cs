using System.Net;
using Microsoft.EntityFrameworkCore;
using Asisya.Models;
using Asisya.Middleware;

namespace Asisya.Data.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _context.Categories!.ToListAsync();
    }

    public async Task<Category?> GetById(int id)
    {
        return await _context.Categories!
            .FirstOrDefaultAsync(c => c.CategoryID == id);
    }

    public async Task Create(Category category)
    {
        if (category is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { mensaje = "Los datos de la categoría no son válidos" }
            );
        }

        await _context.Categories!.AddAsync(category);
    }

    public async Task Delete(int id)
    {
        var category = await GetById(id);

        if (category is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new { mensaje = $"No se encontró la categoría con id {id}" }
            );
        }

        _context.Categories!.Remove(category);
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync()) >= 0;
    }
}
