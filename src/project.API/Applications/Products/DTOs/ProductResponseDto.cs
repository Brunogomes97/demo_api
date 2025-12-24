
namespace project.API.Applications.Products.DTOs
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;

        public string? Description { get; set; } = null!;
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
