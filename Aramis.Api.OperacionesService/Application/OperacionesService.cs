using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.OperacionesService.Application
{
    public class OperacionesService : IOperacionesService
    {
        private readonly IOperacionesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<SystemEmpresa> _empresa;
        private readonly IGenericRepository<BusOperacionDetalle> _busOperacionDetalle;
        public OperacionesService(IOperacionesRepository repository, IGenericRepository<SystemEmpresa> empresa, IMapper mapper, IGenericRepository<BusOperacionDetalle> busOperacionDetalle)
        {
            _repository = repository;
            _busOperacionDetalle = busOperacionDetalle;
            _empresa = empresa;
            _mapper = mapper;
        }
        public BusOperacionesDto NuevaOperacion(BusOperacionesInsert busoperacionesinsert)
        {
            _repository.Insert(_mapper.Map<BusOperacionesInsert, BusOperacion>(busoperacionesinsert));
            return GetOperacion(busoperacionesinsert.Id.ToString());
        }

        public BusOperacionesDto GetOperacion(string id)
        {
            BusOperacion? op = _repository.Get(id);
            IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
            BusOperacionesDto? dto = _mapper.Map<BusOperacion, BusOperacionesDto>(op);
            dto.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
            dto.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
            dto.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
            dto.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
            dto.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
            dto.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
            dto.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
            return dto;
        }

        public BusOperacionesDto InsertDetalle(BusDetalleOperacionesInsert detalle)
        {
            _busOperacionDetalle.Add(_mapper.Map<BusDetalleOperacionesInsert, BusOperacionDetalle>(detalle));
            return GetOperacion(detalle.OperacionId.ToString());
        }

        public BusOperacionesDto DeleteDetalle(string id)
        {
            //TODO solo si se trata de un remito y orden abierta
            BusOperacionDetalle? det = _busOperacionDetalle.Get(Guid.Parse(id));
            _busOperacionDetalle.Delete(Guid.Parse(id));
            return GetOperacion(det.OperacionId.ToString());
        }

        public BusOperacionesDto UpdateDetalle(BusDetalleOperacionesInsert detalle)
        {
            //TODO solo si se trata de un presupuesto o una orden abierta 
            _busOperacionDetalle.Update(_mapper.Map<BusDetalleOperacionesInsert, BusOperacionDetalle>(detalle));
            return GetOperacion(detalle.OperacionId.ToString());
        }

        public bool DeleteOperacion(string operacionid)
        {
            //TODO solo si se trata de un presupuesto o una orden abierta

            throw new NotImplementedException();
        }
    }
}
