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
    }
}
