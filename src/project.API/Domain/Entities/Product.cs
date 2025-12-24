using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project.API.Applications.Products.DTOs;

namespace project.API.Domain.Entities
{
    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; } = null!;
        public string? Description { get; set; } 

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public void Apply(UpdateProductDto dto)
        {
            if (dto.Name is not null)
                Name = dto.Name;

            if (dto.Price.HasValue)
                Price = dto.Price.Value;

            if (dto.Category is not null)
                Category = dto.Category;

            if (dto.Description is not null)
                Description = dto.Description;

            UpdatedAt = DateTime.UtcNow;
        }
    }
}
