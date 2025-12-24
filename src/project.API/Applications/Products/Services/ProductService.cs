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
                Description = dto.Description,
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

        public async Task<ProductResponseDto> UpdateAsync(Guid userId, Guid productId, UpdateProductDto dto)
        {
            var product = await _repo.GetByIdAsync(productId)
                ?? throw new NotFoundException("Product not found");

            if (product.UserId != userId)
                throw new UnauthorizedException("Cannot edit this product");

            product.Apply(dto);

            var updated = await _repo.UpdateAsync(product);

            return new ProductResponseDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Price = updated.Price,
                Category = updated.Category,
                UserId = updated.UserId,
                Description = updated.Description,
                CreatedAt = updated.CreatedAt
            };
        }

        public async Task DeleteAsync(Guid userId, Guid productId)
        {
            var product = await _repo.GetByIdAsync(productId)
                ?? throw new NotFoundException("Product not found");

            if (product.UserId != userId)
                throw new UnauthorizedException("Cannot delete this product");

            await _repo.DeleteAsync(product);
        }

        public async Task<int> CountByUserAsync(Guid userId)
        {
            return await _repo.CountByUserAsync(userId);
        }

        public async Task<PaginatedProductsDto> GetByUserPagedAsync(Guid userId, ProductPaginationQueryDto query)
        {
            if (query.Offset < 0 || query.Limit <= 0)
                throw new InvalidPaginationException("Invalid pagination parameters");
        
            var (items, total) = await _repo.GetPagedByUserAsync(
                userId,
                query.Offset,
                query.Limit,
                query.Category,
                query.Name);

           return new PaginatedProductsDto
            {
                Total = total,
                Items = items.Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.Category,
                    UserId = p.UserId,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                }).ToList()
            };
        }
    }
}