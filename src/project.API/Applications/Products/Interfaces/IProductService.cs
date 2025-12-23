using project.API.Applications.Products.DTOs;

namespace project.API.Applications.Products.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDto> CreateAsync(Guid userId, CreateProductDto dto);
        Task<IEnumerable<ProductResponseDto>> GetByUserAsync(Guid userId);
        Task<ProductResponseDto> UpdateAsync(Guid userId, Guid productId, CreateProductDto dto);
        Task DeleteAsync(Guid userId, Guid productId);
    }
}
