using Asisya.Models;

namespace Asisya.Data.Shippers;

public interface IShipperRepository
{
    Task<IEnumerable<Shipper>> GetAll();
    Task<Shipper?> GetById(int id);
    Task Create(Shipper shipper);
    Task Delete(int id);
    Task<bool> SaveChanges();
}
