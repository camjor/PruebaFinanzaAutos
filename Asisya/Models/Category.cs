using System.ComponentModel.DataAnnotations;

namespace Asisya.Models;

public class Category
{
    [Key]
    public int CategoryID { get; set; }

    [Required]
    public string CategoryName { get; set; }

    public string? Description { get; set; }
    public byte[]? Picture { get; set; }

    // Navigation
    public ICollection<Product>? Products { get; set; }
}
