using Asisya.Models;

namespace Asisya.Data.Suppliers;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAll();
    Task<Supplier?> GetById(int id);
    Task Create(Supplier supplier);
    Task Delete(int id);
    Task<bool> SaveChanges();
}
