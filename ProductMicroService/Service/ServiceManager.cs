using AutoMapper;
using Contracts;
using Service.Contract;
using Sieve.Services;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper, sieveProcessor));
        }
        public IProductService ProductService => _productService.Value;
    }
}
