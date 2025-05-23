using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.UserDto;

namespace UserService.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, UserDto>();
            CreateMap<UserForUpdateDto, UserDto>();

        }
    }
}
