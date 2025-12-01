using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asisya.Models;

public class OrderDetail
{
    public int OrderID { get; set; }
    public int ProductID { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }
    public float Discount { get; set; }

    // Navigation
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
