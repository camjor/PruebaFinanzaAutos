using Asisya.Models;

namespace Asisya.Data.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task Create(Product product);
    Task BulkCreate(int count, int? categoryId, int? supplierId);
    Task Delete(int id);
    Task<bool> SaveChanges();
}
