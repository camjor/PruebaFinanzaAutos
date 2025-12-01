using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asisya.Models;

public class Order
{
    [Key]
    public int OrderID { get; set; }

    // FK
    public string? CustomerID { get; set; }
    public int? EmployeeID { get; set; }
    public int? ShipVia { get; set; }

    // Fechas
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }

    // Envío
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Freight { get; set; }

    public string? ShipName { get; set; }
    public string? ShipAddress { get; set; }
    public string? ShipCity { get; set; }
    public string? ShipRegion { get; set; }
    public string? ShipPostalCode { get; set; }
    public string? ShipCountry { get; set; }

    // Navigation
    public Customer? Customer { get; set; }
    public Employee? Employee { get; set; }
    public Shipper? Shipper { get; set; }

    public ICollection<OrderDetail>? OrderDetails { get; set; }
}
