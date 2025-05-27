using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.ProductDto;

namespace ProductService.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<ProductForCreationDto, Product>().ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<ProductForUpdateDto, Product>().ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
