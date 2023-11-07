using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.OperacionesService.Models;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using AutoMapper;
using System.Linq.Expressions;
using static Aramis.Api.Repository.Helpers.EstadoDocumentos;
using static Aramis.Api.Repository.Helpers.TipoDocumentos;

namespace Aramis.Api.OperacionesService.Interfaces
{
    public class PresupuestoService : OperacionTemplate
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SystemEmpresa> _repositoryEm;
        private readonly IRepository<BusEstado> _busEstado;
        private readonly IRepository<BusOperacionTipo> _busTipo;
        private readonly ISystemService _system;
        private readonly ICustomersService _customers;

        public PresupuestoService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<SystemEmpresa> repositoryEm, IRepository<BusEstado> busEstado, IRepository<BusOperacionTipo> busTipo, ISystemService system, ICustomersService customers) : base(unitOfWork)
        {
            _mapper = mapper;
            _repositoryEm = repositoryEm;
            _busEstado = busEstado;
            _busTipo = busTipo;
            _system = system;
            _customers = customers;
        }

        public async override Task<int> DeleteOperacion(Guid id)
        {
            BusOperacion operacion = await base.Get(id);
            return await base.Delete(operacion);
        }

        public async override Task<BusOperacionesDto> GetOperacion(Guid id)
        {
            Expression<Func<BusOperacion, object>>[] includeProperties = new Expression<Func<BusOperacion, object>>[]
          {
            o => o.Estado,
            o => o.Cliente,
            o => o.Cliente.RespNavigation,
            o => o.BusOperacionDetalles,
            o => o.BusOperacionObservacions,
            o => o.TipoDoc
          };

            BusOperacionesDto operacionDto = _mapper.Map<BusOperacionesDto>(await base.Get(id, includeProperties));
            operacionDto.Empresa = _mapper.Map<SysEmpresaDto>(_repositoryEm.GetAll().Take(1));
            return operacionDto;
        }
        public async override Task<BusOperacionesDto> NuevaOperacion(BusOperacionBaseDto? busoperacionesinsert, string operador)
        {
            SystemIndex index = _system.GetIndex();
            OpCliente cliente = _mapper.Map<OpCliente>(await _customers.GetByCui("0"));
            BusOperacionBaseDto operacion = new()
            {
                Operador = operador,
                CodAut = "",
                ClienteId = cliente.Id,
                EstadoId = _busEstado.GetAll().Where(x => x.Name == Estado.ABIERTO.Name).FirstOrDefault()!.Id,
                Numero = index.Presupuesto += 1,
                Fecha = DateTime.Now,
                Razon = cliente.Razon,
                Pos = 0,
                TipoDocId = _busTipo.GetAll().Where(x => x.Name == TipoDocumento.PRESUPUESTO.Name).FirstOrDefault()!.Id,
                Vence = DateTime.Now,
                Id = Guid.NewGuid()
            };
            await base.Add(_mapper.Map<BusOperacion>(operacion));
            return await GetOperacion(operacion.Id);
        }

        public async override Task<BusOperacionesDto> UpdateOperacion(BusOperacionBaseDto busoperacionesinsert)
        {
            await Update(_mapper.Map<BusOperacion>(busoperacionesinsert));
            return await GetOperacion(busoperacionesinsert.Id);
        }

    }
}
