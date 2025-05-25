using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository
    {
        Task<Product?> GetProductForUserAsync(Guid id, Guid userId, bool trackChanges);
        Task<IEnumerable<Product>> GetProductsForUserAsync(Guid userId, bool trackChanges);
        void CreateProduct(Guid userId, Product product);
        void DeleteProduct(Product product);
    }
}
