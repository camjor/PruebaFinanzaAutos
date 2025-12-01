using Asisya.Models;

namespace Asisya.Data.Employees;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAll();
    Task<Employee?> GetById(int id);
    Task Create(Employee employee);
    Task Delete(int id);
    Task<bool> SaveChanges();
}
