namespace Asisya.Dtos.OrderDtos;

public class OrderRequestDto
{
    public string? CustomerID { get; set; }
    public int? EmployeeID { get; set; }
    public int? ShipVia { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public decimal? Freight { get; set; }

    public string? ShipName { get; set; }
    public string? ShipAddress { get; set; }
    public string? ShipCity { get; set; }
    public string? ShipRegion { get; set; }
    public string? ShipPostalCode { get; set; }
    public string? ShipCountry { get; set; }

    // No se incluyen OrderDetails aquí (se agregarán por un endpoint separado)
}
