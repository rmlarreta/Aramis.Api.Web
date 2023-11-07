using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FlowService.Application
{
    public class MovimientosCuentasService : Service<CobCuentaMovimiento>, IMovimientosCuentasService
    {
        private readonly IMapper _mapper;
        public MovimientosCuentasService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        { 
            _mapper = mapper;
        }

        public async Task Insert(CobCuentaMovimientoDto movimiento)
        {
          await  Add(_mapper.Map<CobCuentaMovimiento>(movimiento));
        }
    }
}
