using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.SecurityService.Helpers
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SecUser, UserDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleNavigation.Name))
            .ReverseMap();

            CreateMap<SecRole, RoleDto>().ReverseMap();
        }
    }
}
