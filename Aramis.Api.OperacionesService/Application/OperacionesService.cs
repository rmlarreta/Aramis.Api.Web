using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.OperacionesService.Models;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.OperacionesService.Application
{
    public abstract class OperacionesService : IOperacionesService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SystemEmpresa> _repositoryEm;
        private readonly ICustomersService _customers;
        private readonly ISystemService _system;
        private readonly IRepository<BusEstado> _busEstado;
        private readonly IRepository<BusOperacionTipo> _busTipo;
        private readonly IUnitOfWork _unitOfWork;
        public OperacionesService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRepository<SystemEmpresa> repositoryEm,
            ICustomersService customers,
            ISystemService system,
            IRepository<BusEstado> busEstado,
            IRepository<BusOperacionTipo> busTipo
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repositoryEm = repositoryEm;
            _customers = customers;
            _system = system;
            _busEstado = busEstado;
            _busTipo = busTipo;
        }

        public Task<int> DeleteOperacion(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BusOperacionesDto> GetOperacion(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<BusOperacionesDto> NuevaOperacion(BusOperacionBaseDto? operacionDto, string operacion)
        {
            OperacionTemplate factory = operacion switch
            {
                "REMITO" => new RemitoService(_unitOfWork, _mapper, _repositoryEm, _busEstado, _busTipo, _system, _customers),
                _ => new PresupuestoService(_unitOfWork, _mapper, _repositoryEm, _busEstado, _busTipo, _system, _customers),
            };
           
            return operacionDto == null ?   await factory.NuevaOperacion(null, operacion) :  await factory.NuevaOperacion(operacionDto, operacion);           
        }

        public Task<BusOperacionesDto> UpdateOperacion(BusOperacionBaseDto busoperacionesinsert)
        {
            throw new NotImplementedException();
        }
    }
}