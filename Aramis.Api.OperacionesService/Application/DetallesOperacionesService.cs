using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using AutoMapper;
using System.Linq.Expressions;

namespace Aramis.Api.OperacionesService.Application
{
    public class DetallesOperacionesService : Service<BusOperacionDetalle>, IDetallesOperacionService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BusOperacion> _repositoryOp;
        private readonly IRepository<SystemEmpresa> _repositoryEm; 
        public DetallesOperacionesService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<BusOperacion> repositoryOp, IRepository<SystemEmpresa> repositoryEm) : base(unitOfWork)
        {
            _mapper = mapper;
            _repositoryOp = repositoryOp;
            _repositoryEm = repositoryEm;
        }

        public async Task<BusOperacionesDto> DeleteDetalles(List<BusDetalleOperacionBase> detalles)
        {

            Expression<Func<BusOperacion, object>>[] includeProperties = new Expression<Func<BusOperacion, object>>[]
            {
            o => o.Estado
            };

            BusOperacion operacion = await _repositoryOp.Get(detalles.First().OperacionId, includeProperties);
            if (operacion.Estado.Name != "ABIERTO") throw new Exception("No se pudo completar la operación");

            await base.Delete(_mapper.Map<List<BusOperacionDetalle>>(detalles));

            includeProperties = new Expression<Func<BusOperacion, object>>[]
            {
            o => o.Estado,
            o => o.Cliente, 
            o => o.Cliente.RespNavigation,
            o => o.BusOperacionDetalles,
            o => o.BusOperacionObservacions,
            o => o.TipoDoc
            };

            BusOperacionesDto operacionDto = _mapper.Map<BusOperacionesDto>(await _repositoryOp.Get(operacion.Id, includeProperties));
            operacionDto.Empresa = _mapper.Map<SysEmpresaDto>(_repositoryEm.GetAll().Take(1));
            return operacionDto;
        }

        public async Task<BusOperacionesDto> InsertDetalles(List<BusDetalleOperacionBase> detalles)
        {
            await base.Add(_mapper.Map<List<BusOperacionDetalle>>(detalles));
            Expression<Func<BusOperacion, object>>[] includeProperties = new Expression<Func<BusOperacion, object>>[]
            {
            o => o.Estado,
            o => o.Cliente,
            o => o.Cliente.RespNavigation,
            o => o.BusOperacionDetalles,
            o => o.BusOperacionObservacions,
            o => o.TipoDoc
            };

           return _mapper.Map<BusOperacionesDto>(await _repositoryOp.Get(detalles.First().OperacionId, includeProperties));
        }

        public async Task<BusOperacionesDto> UpdateDetalles(List<BusDetalleOperacionBase> detalles)
        {
            await base.UpdateRange(_mapper.Map<List<BusOperacionDetalle>>(detalles));
            Expression<Func<BusOperacion, object>>[] includeProperties = new Expression<Func<BusOperacion, object>>[]
            {
            o => o.Estado,
            o => o.Cliente,
            o => o.Cliente.RespNavigation,
            o => o.BusOperacionDetalles,
            o => o.BusOperacionObservacions,
            o => o.TipoDoc
            };

            return _mapper.Map<BusOperacionesDto>(await _repositoryOp.Get(detalles.First().OperacionId, includeProperties));
        }
    }
}
