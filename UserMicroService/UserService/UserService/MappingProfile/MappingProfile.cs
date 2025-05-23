using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.UserDto;

namespace UserService.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ForMember(u => u.FullName, opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"));
            CreateMap<UserForCreationDto, UserDto>();
            CreateMap<UserForUpdateDto, UserDto>();
            CreateMap<UserForCreationDto, User>();
        }
    }
}
