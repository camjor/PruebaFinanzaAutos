using Asisya.Models;

namespace Asisya.Data.Categories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAll();
    Task<Category?> GetById(int id);
    Task Create(Category category);
    Task Delete(int id);
    Task<bool> SaveChanges();
}
