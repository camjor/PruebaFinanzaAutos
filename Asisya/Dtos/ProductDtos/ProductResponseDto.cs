namespace Asisya.Dtos.ProductDtos;

public class ProductResponseDto
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int? SupplierID { get; set; }
    public int? CategoryID { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public bool Discontinued { get; set; }

    // Datos opcionales para el frontend
    public string? CategoryName { get; set; }
    public string? SupplierName { get; set; }
}
