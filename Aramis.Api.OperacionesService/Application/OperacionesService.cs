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
        private readonly IGenericRepository<OpCliente> _opClientes;
        public OperacionesService(
            IOperacionesRepository repository,
            IGenericRepository<SystemEmpresa> empresa,
            IMapper mapper,
            IGenericRepository<BusOperacionDetalle> busOperacionDetalle,
            IGenericRepository<BusOperacionObservacion> busOperacionObservacion,
            IGenericRepository<OpCliente> opClientes
            )
        {
            _repository = repository;
            _busOperacionDetalle = busOperacionDetalle;
            _busOperacionObservacion = busOperacionObservacion;
            _opClientes = opClientes;
            _empresa = empresa;
            _mapper = mapper;
        }

        public BusOperacionesDto GetOperacion(string id)
        {
            BusOperacion? op = _repository.Get(id);
            BusOperacionesDto? dto = _mapper.Map<BusOperacion, BusOperacionesDto>(op);
            IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
            dto.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
            dto.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
            dto.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
            dto.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
            dto.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
            dto.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
            dto.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
            return dto;
        }

        public BusOperacionesDto InsertDetalle(List<BusDetalleOperacionesInsert> detalle)
        {
            detalle.ForEach(
                x =>
                {
                    if (!OperacionEstado(x.OperacionId.ToString()!, "ABIERTO")) throw new Exception("No se pudo completar la operación");
                    x.Id = Guid.NewGuid();
                }); 
          
                _busOperacionDetalle.Add(_mapper.Map<List<BusDetalleOperacionesInsert>, List<BusOperacionDetalle>>(detalle));
                _busOperacionDetalle.Save();
                return GetOperacion(detalle.OrderBy(x=>x.Id).First().OperacionId.ToString());
           
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
            if (OperacionEstado(detalle.OperacionId.ToString()!, "ABIERTO"))
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
                List<BusOperacionDetalle>? dets = _busOperacionDetalle.Get().Where(x => x.OperacionId.Equals(Guid.Parse(operacionid))).ToList();

                _repository.DeleteDetalles(dets);
                _repository.DeleteOperacion(operacionid);
                return _repository.Save();
            }
            throw new Exception("No se pudo completar la operación");
        }
        public BusOperacionesDto UpdateOperacion(BusOperacionesInsert busoperacionesinsert) //Orden o Presupuesto
        {
            if (!OperacionEstado(busoperacionesinsert.Id.ToString()!, "ABIERTO"))
            {
                throw new Exception("No puden modificarse las operaciones confirmadas");
            }

            if (
                TipoOperacion(busoperacionesinsert.Id.ToString()!).Equals("O")
                &
                CuitOperacion(busoperacionesinsert.Id.ToString()!).Equals("0")
              )
            {
                throw new Exception("No se puede asignar este CUI a este tipo de Operaciones");
            }

            var razon = _opClientes.Get(busoperacionesinsert.ClienteId!.Value);
            busoperacionesinsert.Razon = razon.Razon;

            _repository.Update(_mapper.Map<BusOperacionesInsert, BusOperacion>(busoperacionesinsert));
            if (_repository.Save())
            {
                return GetOperacion(busoperacionesinsert.Id.ToString()!);
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
            BusOperacionDetalle? data = _busOperacionDetalle.Get(Guid.Parse(id));
            return _repository.Get(data.OperacionId.ToString()).Estado.Name.Contains(status);
        }
        public bool OperacionEstado(string id, string status)
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

        public async Task<BusOperacionesDto> NuevoRemito(string id)
        {
            if (OperacionEstado(id, "ABIERTO"))
            {
                throw new Exception("No puede emitirse un remito en el estado actual del documento");
            }

            if (OperacionEstado(id, "ENTREGADO"))
            {
                throw new Exception("Ya se ha realizado un remito sobre este documento");
            }

            if (!((TipoOperacion(id).Equals("P")
               ||
               TipoOperacion(id).Equals("O"))
               &&
               OperacionEstado(id, "PAGADO")))
            {
                throw new Exception("No existen pagos asociados a ese documento. Impute un pago o realice la cobranza");
            }
            BusOperacion? operacion = _repository.Get(id);
            List<StockProduct> products = new();
            foreach (BusDetallesOperacionesDto? det in _mapper.Map<BusOperacion, BusOperacionesDto>(operacion).Detalles!)
            {
                StockProduct? product = _repository.GetProducts().Where(x => x.Id.Equals(det.ProductoId)).Where(x => x.Servicio == false).FirstOrDefault();
                if (product != null)
                {
                    product.Cantidad -= det.Cantidad;
                    products.Add(product);
                }
            }
            operacion.Estado = _repository.GetEstados().Where(x => x.Name.Equals("ENTREGADO")).FirstOrDefault()!;
            operacion.TipoDoc = _repository.GetTipos().Where(x => x.Code!.Equals("X")).FirstOrDefault()!;
            SystemIndex? index = _repository.GetIndexs();
            operacion.Numero = index.Remito += 1;
            _repository.UpdateIndexs(index);
            _repository.Update(operacion);
            _repository.Save();
            return await Task.FromResult(GetOperacion(operacion.Id.ToString()));
        }
        public List<BusOperacionesDto> RemitosPendientes()
        {
            List<BusOperacion>? remitos = _repository.Get()
                        .OrderBy(x => x.Cliente.Razon)
                        .Where(x => x.Estado.Name.Equals("ENTREGADO"))
                        .Where(x => x.TipoDoc.Code!.Equals("X"))
                        .Where(x => x.Cliente.Cui != "0")
                        .Union(_repository.Get()
                        .Where(x => x.Estado.Name.Equals("ENTREGADO"))
                        .Where(x => x.TipoDoc.Code!.Equals("X"))
                        .Where(x => x.Cliente.Cui.Equals("0") & DateTime.Now.Date.Subtract((x.Fecha).Date).Days <= 3))
                        .ToList();
            List<BusOperacionesDto>? dto = _mapper.Map<List<BusOperacion>, List<BusOperacionesDto>>(remitos);
            List<BusOperacionesDto>? dtoEnCero = new();
            IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
            foreach (BusOperacionesDto? item in dto)
            {
                if (item.Total.Equals(0.0m))
                {
                    dtoEnCero.Add(item);
                }
                else
                {
                    item.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
                    item.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
                    item.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
                    item.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
                    item.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
                    item.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
                    item.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
                }
            }
            foreach (BusOperacionesDto? item in dtoEnCero)
            {
                dto.Remove(item);
            }
            return dto;
        }
        #endregion

        #region Presupuestos
        public BusOperacionesDto NuevaOperacion(string operador)
        {
            SystemIndex? index = _repository.GetIndexs();
            BusOperacionesInsert busoperacionesinsert = new()
            {
                Id = Guid.NewGuid(),
                Operador = operador,
                CodAut = "",
                ClienteId = _opClientes.Get().Where(x => x.Cui == "0").First().Id,
                TipoDocId = _repository.GetTipos().Where(x => x.Name == "PRESUPUESTO").First().Id,
                EstadoId = _repository.GetEstados().Where(x => x.Name == "ABIERTO").First().Id,
                Fecha =DateTime.Now,
                Numero = index.Presupuesto += 1,
                Pos=0,
                Vence=DateTime.Now,
                Razon = _opClientes.Get().Where(x => x.Cui == "0").First().Razon
            }; 
          
            _repository.Insert(_mapper.Map<BusOperacionesInsert, BusOperacion>(busoperacionesinsert));
            _repository.UpdateIndexs(index);
            _repository.Save();
            return GetOperacion(busoperacionesinsert.Id.ToString()!);
        }
        public List<BusOperacionesDto> Presupuestos()
        {
            List<BusOperacion>? presupuestos = _repository.Get()
                        .OrderBy(x => x.Cliente.Razon)
                        .Where(x => x.Estado.Name.Equals("ABIERTO"))
                        .Where(x => x.TipoDoc.Code!.Equals("P"))
                        .Where(x => DateTime.Now.Date.Subtract((x.Fecha).Date).Days <= 15)
                        .ToList();
            List<BusOperacionesDto>? dto = _mapper.Map<List<BusOperacion>, List<BusOperacionesDto>>(presupuestos);
            IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
            foreach (BusOperacionesDto? item in dto)
            {
                item.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
                item.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
                item.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
                item.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
                item.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
                item.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
                item.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
            }
            return dto;
        }

        #endregion
        #region Ordenes
        public BusOrdenesTicketDto NuevaOrden(string id)
        {
            if (!(TipoOperacion(id).Equals("P")
              &&
              OperacionEstado(id, "ABIERTO")))
            {
                throw new Exception("Este documento no puede pasar a Orden");
            }
            BusOperacion? operacion = _repository.Get(id);
            SystemIndex? index = _repository.GetIndexs();
            operacion.Numero = index.Orden += 1;
            _repository.UpdateIndexs(index);
            _repository.Update(operacion);
            _repository.Save();
            return _mapper.Map<BusOperacion, BusOrdenesTicketDto>(operacion);
        }
        public List<BusOperacionesDto> OrdenesByEstado(string estado)
        {
            List<BusOperacion>? ordenes = _repository.Get()
                        .OrderBy(x => x.Cliente.Razon)
                        .Where(x => x.Estado.Id.Equals(estado))
                        .Where(x => x.TipoDoc.Code!.Equals("O"))
                        .ToList();
            List<BusOperacionesDto>? dto = _mapper.Map<List<BusOperacion>, List<BusOperacionesDto>>(ordenes);
            IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
            foreach (BusOperacionesDto? item in dto)
            {
                item.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
                item.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
                item.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
                item.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
                item.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
                item.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
                item.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
            }
            return dto;
        }
        #endregion

        #region auxiliares
        public List<BusOperacionTipoDto> TipoOperacions()
        {
            List<BusOperacionTipo> tipos = _repository.GetTipos();
            return _mapper.Map<List<BusOperacionTipo>, List<BusOperacionTipoDto>>(tipos);
        }

        public List<BusEstadoDto> Estados()
        {
            List<BusEstado> estados = _repository.GetEstados();
            return _mapper.Map<List<BusEstado>, List<BusEstadoDto>>(estados);
        }

        #endregion
    }
}
