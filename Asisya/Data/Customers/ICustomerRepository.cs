using Asisya.Models;

namespace Asisya.Data.Customers;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> GetById(string id);
    Task Create(Customer customer);
    Task Delete(string id);
    Task<bool> SaveChanges();
}
