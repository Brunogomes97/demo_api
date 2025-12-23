using project.API.Applications.Products.DTOs;
using project.API.Applications.Products.Interfaces;
using project.API.Domain.Entities;
using project.API.Domain.Exceptions;

namespace project.API.Applications.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo) => _repo = repo;

        public async Task<ProductResponseDto> CreateAsync(Guid userId, CreateProductDto dto)
        {
            var product = new ProductEntity
            {
                Name = dto.Name,
                Price = dto.Price,
                Category = dto.Category,
                UserId = userId
            };

            var created = await _repo.AddAsync(product);

            return new ProductResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Price = created.Price,
                Category = created.Category,
                UserId = created.UserId,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<IEnumerable<ProductResponseDto>> GetByUserAsync(Guid userId)
        {
            var products = await _repo.GetByUserIdAsync(userId);
            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                UserId = p.UserId,
                CreatedAt = p.CreatedAt
            });
        }

        public async Task<ProductResponseDto> UpdateAsync(Guid userId, Guid productId, CreateProductDto dto)
        {
            var product = await _repo.GetByIdAsync(productId)
                ?? throw new NotFoundException("Product not found");

            if (product.UserId != userId)
                throw new UnauthorizedException("Cannot edit another user's product");

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Category = dto.Category;

            var updated = await _repo.UpdateAsync(product);

            return new ProductResponseDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Price = updated.Price,
                Category = updated.Category,
                UserId = updated.UserId,
                CreatedAt = updated.CreatedAt
            };
        }

        public async Task DeleteAsync(Guid userId, Guid productId)
        {
            var product = await _repo.GetByIdAsync(productId)
                ?? throw new NotFoundException("Product not found");

            if (product.UserId != userId)
                throw new UnauthorizedException("Cannot delete another user's product");

            await _repo.DeleteAsync(product);
        }
    }
}
