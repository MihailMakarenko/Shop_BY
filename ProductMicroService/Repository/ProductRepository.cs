using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        public IQueryable<Product> GetAllProducts(bool trackChanges)
        {
            var products = FindAll(trackChanges);

            return products;
        }

        public async Task<Product?> GetProductForUserAsync(Guid id, Guid userId, bool trackChanges)
        {
            var product = await FindByCondition(p => p.Id.Equals(id) && p.CreatedByUserId!.Equals(userId.ToString()), trackChanges).SingleOrDefaultAsync();

            return product;
        }

        public IQueryable<Product> GetProductsForUser(Guid userId, bool trackChanges)
        {
            var userIdStr = userId.ToString();
            var products = FindByCondition(p => p.CreatedByUserId == userIdStr && p.IsAvailable , trackChanges);

            return products;
        }

        public IQueryable<Product> GetProductsForChangedAvailable(Guid userId, bool trackChanges)
        {
            var products = FindByCondition(p => p.CreatedByUserId == userId.ToString(), trackChanges);

            return products;
        }
    }
}
