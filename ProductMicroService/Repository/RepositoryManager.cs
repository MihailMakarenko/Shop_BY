using Contracts;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _appDbContext;

        private readonly Lazy<IProductRepository> _productRepository;

        public RepositoryManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(appDbContext));
        }
        public IProductRepository Product => _productRepository.Value;

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
