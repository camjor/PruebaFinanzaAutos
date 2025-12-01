using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asisya.Models;

public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    public string ProductName { get; set; }

    // Foreign Keys
    public int? SupplierID { get; set; }
    public int? CategoryID { get; set; }

    // Datos del producto
    public string? QuantityPerUnit { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }

    // Navigation
    public Supplier? Supplier { get; set; }
    public Category? Category { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}
