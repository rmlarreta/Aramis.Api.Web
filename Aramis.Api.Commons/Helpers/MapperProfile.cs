using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.Commons.ModelsDto.Stock;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.Commons.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OpCliente, OpClienteDto>()
            .ForMember(dest => dest.PaisName, opt => opt.MapFrom(src => src.PaisNavigation.Name))
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.GenderNavigation.Name))
            .ForMember(dest => dest.RespName, opt => opt.MapFrom(src => src.RespNavigation.Name))
            .ReverseMap();

            CreateMap<OpCliente, OpClienteInsert>().ReverseMap();

            CreateMap<SecUser, UserDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleNavigation.Name))
            .ReverseMap();

            CreateMap<SecRole, RoleDto>().ReverseMap();

            CreateMap<StockProduct, StockProductDto>()
          .ForMember(dest => dest.RubroName, opt => opt.MapFrom(src => src.RubroNavigation.Name))
          .ForMember(dest => dest.IvaValue, opt => opt.MapFrom(src => src.IvaNavigation.Value))
          .ReverseMap();

            CreateMap<StockProduct, StockProductInsert>() 
          .ReverseMap();

        }
    }
}
