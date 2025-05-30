using Entities.Models;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace ProductService.Application.ProductFeatures.Queries.GetFilteredSortedProducts;

public class ApplicationSieveProcessor : SieveProcessor
{
    public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options) { }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        mapper.Property<Product>(product => product.Price).CanFilter().CanSort();
        mapper.Property<Product>(product => product.Name).CanFilter().CanSort();
        mapper.Property<Product>(product => product.CreatedAt).CanFilter().CanSort();
        mapper.Property<Product>(product => product.UpdatedAt).CanFilter().CanSort();
        mapper.Property<Product>(product => product.IsAvailable).CanFilter();
        mapper.Property<Product>(product => product.CreatedByUserId).CanFilter();

    //    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    //{
    //    // Для поиска по точному совпадению
    //    mapper.Property<Product>(p => p.Name)
    //        .CanFilter()
    //        .HasName("name");

    //    // Для поиска по подстроке (содержит)
    //    mapper.Property<Product>(p => p.Description)
    //        .CanFilter()
    //        .HasName("description")
    //        .CanContains(); // Включаем поиск по подстроке

    //    // Для поиска по началу строки
    //    mapper.Property<Product>(p => p.Name)
    //        .CanStartsWith()
    //        .HasName("name_start");

    //    return mapper;
    //}

        return mapper;
    }
}