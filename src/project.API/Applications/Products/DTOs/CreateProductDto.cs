using System.ComponentModel.DataAnnotations;

namespace project.API.Applications.Products.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
