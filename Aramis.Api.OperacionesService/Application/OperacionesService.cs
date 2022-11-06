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
        private readonly IGenericRepository<BusOperacionObservacion> _busOperacionObservacion;
        public OperacionesService(
            IOperacionesRepository repository,
            IGenericRepository<SystemEmpresa> empresa,
            IMapper mapper, 
            IGenericRepository<BusOperacionDetalle> busOperacionDetalle,
            IGenericRepository<BusOperacionObservacion> busOperacionObservacion
            )
        {
            _repository = repository;
            _busOperacionDetalle = busOperacionDetalle;
            _busOperacionObservacion = busOperacionObservacion;
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
            if (DetalleAbierto(detalle.Id.ToString()))
            {
                _busOperacionDetalle.Add(_mapper.Map<BusDetalleOperacionesInsert, BusOperacionDetalle>(detalle));
                return GetOperacion(detalle.OperacionId.ToString());
            }
            throw new Exception("No se pudo completar la operación");
        }

        public BusOperacionesDto DeleteDetalle(string id)
        {
            if (DetalleAbierto(id))
            {
                BusOperacionDetalle? det = _busOperacionDetalle.Get(Guid.Parse(id));
                _busOperacionDetalle.Delete(Guid.Parse(id));
                return GetOperacion(det.OperacionId.ToString());
            }
            throw new Exception("No se pudo completar la operación");
        }

        public BusOperacionesDto UpdateDetalle(BusDetalleOperacionesInsert detalle)
        {
            if (DetalleAbierto(detalle.Id.ToString()))
            {
                _busOperacionDetalle.Update(_mapper.Map<BusDetalleOperacionesInsert, BusOperacionDetalle>(detalle));
                return GetOperacion(detalle.OperacionId.ToString());
            }
            throw new Exception("No se pudo completar la operación");
        }

        public bool DeleteOperacion(string operacionid)
        {
            if (OperacionAbierta(operacionid))
            {
                var dets = _busOperacionDetalle.Get().Where(x => x.OperacionId.Equals(operacionid)).ToList();
                if (_repository.DeleteDetalles(dets))
                {
                    return _repository.DeleteOperacion(operacionid);
                }
                throw new Exception("No se pudo completar la operación");
            }
            throw new Exception("No se pudo completar la operación");
        }
        public BusOperacionesDto UpdateOperacion(BusOperacionesInsert busoperacionesinsert)
        {
            if (!OperacionAbierta(busoperacionesinsert.Id.ToString())) throw new Exception("No puden modificarse las operaciones confirmadas");
            if (
                TipoOperacion(busoperacionesinsert.Id.ToString()).Equals("O")
                &
                CuitOperacion(busoperacionesinsert.Id.ToString()).Equals("0")
              ) throw new Exception("No se puede asignar este CUI a este tipo de Operaciones");
            if (_repository.Update(_mapper.Map<BusOperacionesInsert, BusOperacion>(busoperacionesinsert)))
            {
                return GetOperacion(busoperacionesinsert.Id.ToString());
            }
            throw new Exception("No se pudo completar la operación");
        }

        #region Observas
        public bool InsertObservacion(BusObservacionesInsert observacion)
        {
           _busOperacionObservacion.Add(_mapper.Map<BusObservacionesInsert, BusOperacionObservacion>(observacion));
            return _busOperacionObservacion.Save();
        }
        public bool DeleteObservacion(string id)
        {
           _busOperacionObservacion.Delete(Guid.Parse(id));
            return _busOperacionObservacion.Save();
        }
        public bool UpdateObservacion(BusObservacionesInsert observacion)
        {
           _busOperacionObservacion.Update(_mapper.Map<BusObservacionesInsert, BusOperacionObservacion>(observacion));
            return _busOperacionObservacion.Save();
        }
        #endregion Observas
        private bool DetalleAbierto(string id)
        {
            var data = _busOperacionDetalle.Get(Guid.Parse(id));
            return _repository.Get(data.OperacionId.ToString()).Estado.Name.Contains("ABIERTO");
        }
        private bool OperacionAbierta(string id)
        {
            return _repository.Get(id).Estado.Name.Contains("ABIERTO");
        }
        private string TipoOperacion(string id)
        {
            return _repository.Get(id).TipoDoc.Code!.ToString();
        }
        private string CuitOperacion(string id)
        {
            return _repository.Get(id).Cliente.Cui.ToString();
        }
    }
}
