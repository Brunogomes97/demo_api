using Microsoft.EntityFrameworkCore;
using project.API.Applications.Products.Interfaces;
using project.API.Domain.Entities;
using project.API.Infrastructure.Data;
namespace project.API.Applications.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) => _db = db;

        public async Task<ProductEntity> AddAsync(ProductEntity product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(ProductEntity product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<ProductEntity?> GetByIdAsync(Guid id) =>
            await _db.Products.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<ProductEntity>> GetByUserIdAsync(Guid userId) =>
            await _db.Products.Where(p => p.UserId == userId).ToListAsync();

        public async Task<ProductEntity> UpdateAsync(ProductEntity product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<int> CountByUserAsync(Guid userId)
        {
            return await _db.Products
                .Where(p => p.UserId == userId)
                .CountAsync();
        }

        public async Task<(IReadOnlyList<ProductEntity>, int)> GetPagedByUserAsync(
            Guid userId,
            int offset,
            int limit,
            string? category,
            string? name)
        {
            var query = _db.Products
                .AsNoTracking()
                .Where(p => p.UserId == userId);

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category == category);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name));

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }


    }
}
