using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.Commons.ModelsDto.Ordenes;
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
            var index = _repository.GetIndexs(); 
            busoperacionesinsert.Numero = index.Presupuesto += 1;
            _repository.Insert(_mapper.Map<BusOperacionesInsert, BusOperacion>(busoperacionesinsert));
            _repository.UpdateIndexs(index);
            _repository.Save();
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
            if (DetalleEstado(detalle.Id.ToString(), "ABIERTO"))
            {
                _busOperacionDetalle.Add(_mapper.Map<BusDetalleOperacionesInsert, BusOperacionDetalle>(detalle));
                _busOperacionDetalle.Save();
                return GetOperacion(detalle.OperacionId.ToString());
            }
            throw new Exception("No se pudo completar la operación");
        }

        public BusOperacionesDto DeleteDetalle(string id)
        {
            if (DetalleEstado(id, "ABIERTO"))
            {
                BusOperacionDetalle? det = _busOperacionDetalle.Get(Guid.Parse(id));
                _busOperacionDetalle.Delete(Guid.Parse(id));
                _busOperacionDetalle.Save();
                return GetOperacion(det.OperacionId.ToString());
            }
            throw new Exception("No se pudo completar la operación");
        }

        public BusOperacionesDto UpdateDetalle(BusDetalleOperacionesInsert detalle)
        {
            if (DetalleEstado(detalle.Id.ToString(), "ABIERTO"))
            {
                _busOperacionDetalle.Update(_mapper.Map<BusDetalleOperacionesInsert, BusOperacionDetalle>(detalle));
                _busOperacionDetalle.Save();
                return GetOperacion(detalle.OperacionId.ToString());
            }
            throw new Exception("No se pudo completar la operación");
        }

        public bool DeleteOperacion(string operacionid)
        {
            if (OperacionEstado(operacionid, "ABIERTO"))
            {
                var dets = _busOperacionDetalle.Get().Where(x => x.OperacionId.Equals(operacionid)).ToList();

                _repository.DeleteDetalles(dets);
                _repository.DeleteOperacion(operacionid);
                return _repository.Save();
            }
            throw new Exception("No se pudo completar la operación");
        }
        public BusOperacionesDto UpdateOperacion(BusOperacionesInsert busoperacionesinsert) //Orden o Presupuesto
        {
            if (!OperacionEstado(busoperacionesinsert.Id.ToString(), "ABIERTO")) throw new Exception("No puden modificarse las operaciones confirmadas");
            if (
                TipoOperacion(busoperacionesinsert.Id.ToString()).Equals("O")
                &
                CuitOperacion(busoperacionesinsert.Id.ToString()).Equals("0")
              ) throw new Exception("No se puede asignar este CUI a este tipo de Operaciones");

            _repository.Update(_mapper.Map<BusOperacionesInsert, BusOperacion>(busoperacionesinsert));
            if (_repository.Save())
            {
                return GetOperacion(busoperacionesinsert.Id.ToString());
            }
            throw new Exception("No se pudo completar la operación");
        }

        #region Observas
        public bool InsertObservacion(BusObservacionesDto observacion)
        {
            _busOperacionObservacion.Add(_mapper.Map<BusObservacionesDto, BusOperacionObservacion>(observacion));
            return _busOperacionObservacion.Save();
        }
        public bool DeleteObservacion(string id)
        {
            _busOperacionObservacion.Delete(Guid.Parse(id));
            return _busOperacionObservacion.Save();
        }
        public bool UpdateObservacion(BusObservacionesDto observacion)
        {
            _busOperacionObservacion.Update(_mapper.Map<BusObservacionesDto, BusOperacionObservacion>(observacion));
            return _busOperacionObservacion.Save();
        }
        #endregion Observas

        #region Privates
        private bool DetalleEstado(string id, string status)
        {
            var data = _busOperacionDetalle.Get(Guid.Parse(id));
            return _repository.Get(data.OperacionId.ToString()).Estado.Name.Contains(status);
        }
        private bool OperacionEstado(string id, string status)
        {
            return _repository.Get(id).Estado.Name.Contains(status);
        }
        private string TipoOperacion(string id)
        {
            return _repository.Get(id).TipoDoc.Code!.ToString();
        }
        private string CuitOperacion(string id)
        {
            return _repository.Get(id).Cliente.Cui.ToString();
        }
        #endregion

        #region Remitos
        public BusOperacionesDto NuevoRemito(string id)
        { 
            if (OperacionEstado(id, "ABIERTO")) throw new ApplicationException("No puede emitirse un remito en el estado actual del documento");
            if (OperacionEstado(id, "ENTREGADO")) throw new ApplicationException("Ya se ha realizado un remito sobre este documento");
            if (!((TipoOperacion(id).Equals("P")  
               || 
               TipoOperacion(id).Equals("O")) 
               && 
               OperacionEstado(id, "PAGADO")))  
            {
                throw new ApplicationException("No existen pagos asociados a ese documento. Impute un pago o realice la cobranza");
            } 
            var operacion = _mapper.Map<BusOperacion,BusOperacionesDto>(_repository.Get(id));
            List<StockProduct> products = new();
            foreach(var det in operacion.Detalles!)
            {
               var product=_repository.GetProducts().Where(x=>x.Id.Equals(det.ProductoId)).FirstOrDefault();  
                if (product != null)
                {
                    product.Cantidad -= det.Cantidad; 
                    products.Add(product);
                }
            }
            operacion.EstadoId = _repository.GetEstados().Where(x => x.Name.Equals("ENTREGADO")).FirstOrDefault()!.Id;
            var index = _repository.GetIndexs();
            operacion.Numero = index.Remito += 1;
            _repository.UpdateIndexs(index);
            _repository.Save();
            return operacion;
        }

        #endregion

        #region Ordenes
        public BusOrdenesTicketDto NuevaOrden(string id)
        {
             if (!(TipoOperacion(id).Equals("P") 
               &&
               OperacionEstado(id, "ABIERTO")))
            {
                throw new ApplicationException("Este documento no puede pasar a Orden");
            }
            var operacion = _mapper.Map<BusOperacion, BusOperacionesDto>(_repository.Get(id)); 
            var index = _repository.GetIndexs();
            operacion.Numero = index.Orden += 1;
            _repository.UpdateIndexs(index);
            _repository.Save();
            var orden= _mapper.Map<BusOperacion, BusOrdenesTicketDto>(_mapper.Map<BusOperacionesDto, BusOperacion>(operacion)); 
            return orden;
        }
        #endregion
    }
}
