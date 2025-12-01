using System.ComponentModel.DataAnnotations;

namespace Asisya.Models;

public class Shipper
{
    [Key]
    public int ShipperID { get; set; }

    public string CompanyName { get; set; }

    public string? Phone { get; set; }

    // Navigation
    public ICollection<Order>? Orders { get; set; }
}
