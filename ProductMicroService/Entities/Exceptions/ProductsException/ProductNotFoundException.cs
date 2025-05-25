namespace Entities.Exceptions.ProductsException
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid productId) : base($"The Product with " +
          $"id: {productId} doesn't exist in the database.")
        {
        }
    }
}
