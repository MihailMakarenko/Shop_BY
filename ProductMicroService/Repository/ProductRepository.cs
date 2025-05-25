using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }


        public void CreateProduct(Guid userId, Product product)
        {
            product.CreatedByUserId = userId.ToString();
            Create(product);

        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public async Task<Product?> GetProductForUserAsync(Guid id, Guid userId, bool trackChanges)
        {
            var product = await FindByCondition(p => p.Id.Equals(id) && p.CreatedByUserId!.Equals(userId), trackChanges).SingleOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsForUserAsync(Guid userId, bool trackChanges)
        {
            var products = await FindByCondition(p => p.CreatedByUserId!.Equals(userId), trackChanges).ToListAsync();

            return products;
        }
    }
}
