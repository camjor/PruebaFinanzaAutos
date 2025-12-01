using System.ComponentModel.DataAnnotations;

namespace Asisya.Models;

public class Employee
{
    [Key]
    public int EmployeeID { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string FirstName { get; set; }

    public string? Title { get; set; }
    public string? TitleOfCourtesy { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? HireDate { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? HomePhone { get; set; }
    public string? Extension { get; set; }
    public byte[]? Photo { get; set; }
    public string? Notes { get; set; }

    // FK auto referencia
    public int? ReportsTo { get; set; }
    public Employee? Manager { get; set; }

    // Navigation
    public ICollection<Employee>? Subordinates { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
