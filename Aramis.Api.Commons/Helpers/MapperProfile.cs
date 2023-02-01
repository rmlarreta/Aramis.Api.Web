using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.Commons.ModelsDto.Ordenes;
using Aramis.Api.Commons.ModelsDto.Pagos;
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
            #region Clientes
            CreateMap<OpCliente, OpClienteDto>()
          .ForMember(dest => dest.PaisName, opt => opt.MapFrom(src => src.PaisNavigation.Name))
          .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.GenderNavigation.Name))
          .ForMember(dest => dest.RespName, opt => opt.MapFrom(src => src.RespNavigation.Name))
          .ReverseMap();

            CreateMap<OpCliente, OpClienteInsert>().ReverseMap();

            CreateMap<OpResp, OpRespDto>().ReverseMap();
            CreateMap<OpPai, OpPaiDto>().ReverseMap();
            CreateMap<OpGender, OpGenderDto>().ReverseMap();
            #endregion

            #region Security
            CreateMap<SecUser, UserDto>()
           .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleNavigation.Name))
           .ReverseMap();

            CreateMap<SecRole, RoleDto>().ReverseMap();
            #endregion

            #region Stock
            CreateMap<StockProduct, StockProductDto>()
          .ForMember(dest => dest.RubroName, opt => opt.MapFrom(src => src.RubroNavigation.Name))
          .ForMember(dest => dest.IvaValue, opt => opt.MapFrom(src => src.IvaNavigation.Value))
          .ReverseMap();

            CreateMap<StockProduct, StockProductInsert>()
            .ReverseMap();

            CreateMap<StockIva, StockIvaDto>()
         .ReverseMap();

            CreateMap<StockRubro, StockRubroDto>()
         .ReverseMap();

            #endregion

            #region Operaciones
            CreateMap<BusOperacion, BusOperacionesInsert>()
           .ReverseMap();

            CreateMap<BusOperacion, BusOperacionesDto>()
            .ForMember(dest => dest.TipoDocName, opt => opt.MapFrom(src => src.TipoDoc.Name))
            .ForMember(dest => dest.EstadoName, opt => opt.MapFrom(src => src.Estado.Name))
            .ForMember(dest => dest.Cui, opt => opt.MapFrom(src => src.Cliente.Cui))
            .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Cliente.Domicilio))
            .ForMember(dest => dest.Resp, opt => opt.MapFrom(src => src.Cliente.RespNavigation.Name))
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.BusOperacionDetalles))
            .ForMember(dest => dest.Observaciones, opt => opt.MapFrom(src => src.BusOperacionObservacions))
            .ReverseMap();

            CreateMap<BusOperacionDetalle, BusDetallesOperacionesDto>()
            .ForMember(dest => dest.Detalle, opt => opt.MapFrom(src => src.Detalle))
            .ReverseMap();

            CreateMap<BusOperacionDetalle, BusDetalleOperacionesInsert>().ReverseMap();

            CreateMap<BusOperacionTipo, BusOperacionTipoDto>().ReverseMap();
            CreateMap<BusEstado, BusEstadoDto>().ReverseMap();
            CreateMap<BusOperacionPago, BusOperacionPagoDto>().ReverseMap();
            #endregion

            #region Ordenes
            CreateMap<BusOperacion, BusOrdenesTicketDto>()
             .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Cliente.Razon))
             .ForMember(dest => dest.Cui, opt => opt.MapFrom(src => src.Cliente.Cui))
             .ForMember(dest => dest.Observaciones, opt => opt.MapFrom(src => src.BusOperacionObservacions))
             .ReverseMap();
            #endregion 

            #region Observaciones
            CreateMap<BusOperacionObservacion, BusObservacionesDto>()
            .ForMember(dest => dest.Observacion, opt => opt.MapFrom(src => src.Observacion))
            .ReverseMap();
            #endregion

            #region Recibos
            CreateMap<CobRecibo, CobReciboInsert>()
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.CobReciboDetalles))
            .ReverseMap();

            CreateMap<CobRecibo, CobReciboDto>()
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.CobReciboDetalles))
            .ReverseMap();

            CreateMap<CobReciboDetalle, CobReciboDetallesInsert>()
            .ForMember(dest => dest.Cancelado, opt => opt.MapFrom(src => src.TipoNavigation.Name != "CUENTA CORRIENTE"))
            .ReverseMap();
            CreateMap<CobReciboDetalle, CobReciboDetalleDto>()
            .ReverseMap();
            #endregion

            #region Cuentas
            CreateMap<CobCuentum, CobCuentDto>()
           .ReverseMap();
            CreateMap<CobTipoPago, CobTipoPagoDto>()
           .ReverseMap();
            CreateMap<CobPo, CobPosDto>()
           .ReverseMap();
            #endregion
        }
    }
}
