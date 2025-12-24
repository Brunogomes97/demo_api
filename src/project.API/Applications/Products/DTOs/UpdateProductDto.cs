using System.ComponentModel.DataAnnotations;

namespace project.API.Applications.Products.DTOs;

public class UpdateProductDto
{
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Range(0.01, 1_000_000)]
    public decimal? Price { get; set; }

    [MaxLength(50)]
    public string? Category { get; set; }
    public string? Description { get; set; } 
}
