using project.API.Domain.Entities;

namespace project.API.Applications.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductEntity> AddAsync(ProductEntity product);
        Task<ProductEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductEntity>> GetByUserIdAsync(Guid userId);
        Task<ProductEntity> UpdateAsync(ProductEntity product);
        Task<int> CountByUserAsync(Guid userId);
        Task<(IReadOnlyList<ProductEntity>, int)> GetPagedByUserAsync(
            Guid userId,
            int offset,
            int limit,
            string? category,
            string? name);
        Task DeleteAsync(ProductEntity product);
    }
}
