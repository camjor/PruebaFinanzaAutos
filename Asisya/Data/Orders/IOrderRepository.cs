using Asisya.Models;

namespace Asisya.Data.Orders;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAll();
    Task<Order?> GetById(int id);
    Task Create(Order order);
    Task Delete(int id);
    Task<bool> SaveChanges();
}
