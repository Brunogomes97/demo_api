using System.ComponentModel.DataAnnotations;

namespace project.API.Applications.Products.DTOs;

public class CreateProductDto
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must have at least 3 characters")]
    [MaxLength(100, ErrorMessage = "Name must have at most 100 characters")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 1_000_000, ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Category is required")]
    [MaxLength(50)]
    public string Category { get; set; } = default!;

    public string? Description { get; set; }
}
