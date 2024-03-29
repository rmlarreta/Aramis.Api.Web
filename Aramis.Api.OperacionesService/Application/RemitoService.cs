﻿using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.OperacionesService.Application;
using Aramis.Api.OperacionesService.Models;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.OperacionesService.Interfaces
{
    public class RemitoService : OperacionTemplate
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SystemEmpresa> _repositoryEm;
        private readonly IRepository<BusEstado> _busEstado;
        private readonly IRepository<BusOperacionTipo> _busTipo;
        private readonly ISystemService _system;
        private readonly ICustomersService _customers;
        public RemitoService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<SystemEmpresa> repositoryEm, IRepository<BusEstado> busEstado, IRepository<BusOperacionTipo> busTipo, ISystemService system, ICustomersService customers) : base(unitOfWork)
        {
            _mapper = mapper;
            _repositoryEm = repositoryEm;
            _busEstado = busEstado;
            _busTipo = busTipo;
            _system = system;
            _customers = customers;
        }
         
        public override Task<int> DeleteOperacion(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<BusOperacionesDto> GetOperacion(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<BusOperacionesDto> NuevaOperacion(BusOperacionBaseDto? busoperacionesinsert, string operador)
        {
            throw new NotImplementedException();
        }

        public override Task<BusOperacionesDto> UpdateOperacion(BusOperacionBaseDto busoperacionesinsert)
        {
            throw new NotImplementedException();
        }
    }
}
 

//public BusOperacionesDto UpdateOperacion(BusOperacionesInsert busoperacionesinsert) //Orden o Presupuesto
//{
//    if (!OperacionEstado(busoperacionesinsert.Id.ToString()!, "ABIERTO"))
//    {
//        throw new Exception("No puden modificarse las operaciones confirmadas");
//    }

//    if (
//        TipoOperacion(busoperacionesinsert.Id.ToString()!).Equals("O")
//        &
//        CuitOperacion(busoperacionesinsert.Id.ToString()!).Equals("0")
//      )
//    {
//        throw new Exception("No se puede asignar este CUI a este tipo de Operaciones");
//    }

//    var razon = _opClientes.Get(Guid.Parse(busoperacionesinsert.ClienteId!));
//    busoperacionesinsert.Razon = razon.Razon;

//    _repository.Update(_mapper.Map<BusOperacionesInsert, BusOperacion>(busoperacionesinsert));
//    if (_repository.Save())
//    {
//        return GetOperacion(busoperacionesinsert.Id.ToString()!);
//    }
//    throw new Exception("No se pudo completar la operación");
//}

//#region Observas

//public bool InsertObservacion(BusObservacionesDto observacion)
//{
//    _busOperacionObservacion.Add(_mapper.Map<BusObservacionesDto, BusOperacionObservacion>(observacion));
//    return _busOperacionObservacion.Save();
//}
//public bool DeleteObservacion(string id)
//{
//    _busOperacionObservacion.Delete(Guid.Parse(id));
//    return _busOperacionObservacion.Save();
//}
//public bool UpdateObservacion(BusObservacionesDto observacion)
//{
//    _busOperacionObservacion.Update(_mapper.Map<BusObservacionesDto, BusOperacionObservacion>(observacion));
//    return _busOperacionObservacion.Save();
//}

//#endregion Observas

//#region Privates

//private bool DetalleEstado(string id, string status)
//{
//    BusOperacionDetalle? data = _busOperacionDetalle.Get(Guid.Parse(id));
//    return _repository.Get(data.OperacionId.ToString()).Estado.Name.Contains(status);
//}
//public bool OperacionEstado(string id, string status)
//{
//    return _repository.Get(id).Estado.Name.Contains(status);
//}
//private string TipoOperacion(string id)
//{
//    return _repository.Get(id).TipoDoc.Code!.ToString();
//}

//private string CuitOperacion(string id)
//{
//    return _repository.Get(id).Cliente.Cui.ToString();
//}

//#endregion

//#region Remitos

//public async Task<BusOperacionesDto> NuevoRemito(string id)
//{
//    if (OperacionEstado(id, "ABIERTO"))
//    {
//        throw new Exception("No puede emitirse un remito en el estado actual del documento");
//    }

//    if (OperacionEstado(id, "ENTREGADO"))
//    {
//        throw new Exception("Ya se ha realizado un remito sobre este documento");
//    }

//    if (!((TipoOperacion(id).Equals("P")
//       ||
//       TipoOperacion(id).Equals("O"))
//       &&
//       OperacionEstado(id, "PAGADO")))
//    {
//        throw new Exception("No existen pagos asociados a ese documento. Impute un pago o realice la cobranza");
//    }
//    BusOperacionesDto? operacion = _mapper.Map<BusOperacionesDto>(_repository.Get(id));
//    List<StockProduct> products = new();
//    foreach (BusDetallesOperacionesDto? det in operacion.Detalles!)
//    {
//        StockProduct? product = _repository.GetProducts().Where(x => x.Id.Equals(det.ProductoId)).Where(x => x.Servicio == false).FirstOrDefault();
//        if (product != null)
//        {
//            product.Cantidad -= det.Cantidad;
//            products.Add(product);
//        }
//    }
//    operacion.EstadoId = _repository.GetEstados().Where(x => x.Name.Equals("ENTREGADO")).FirstOrDefault()!.Id;
//    operacion.TipoDocId = _repository.GetTipos().Where(x => x.Code!.Equals("X")).FirstOrDefault()!.Id;
//    SystemIndex? index = _repository.GetIndexs();
//    operacion.Numero = index.Remito += 1;
//    _repository.UpdateIndexs(index);
//    _repository.Update(_mapper.Map<BusOperacion>(operacion));
//    _repository.Save();
//    return await Task.FromResult(GetOperacion(operacion.Id.ToString()));
//}

//public async Task<BusOperacionesDto> NuevaDevolucion(List<BusDetalleDevolucion> devolucion)
//{
//    SystemIndex? index = _repository.GetIndexs();
//    BusOperacionesDto? remito = _mapper.Map<BusOperacionesDto>(_repository.Get(devolucion.FirstOrDefault()!.OperacionId.ToString()));
//    BusOperacionesInsert nuevaDevolucion = new()
//    {
//        Numero = index.Remito += 1,
//        ClienteId = remito.ClienteId,
//        CodAut = null,
//        Fecha = DateTime.Now,
//        Operador = devolucion.FirstOrDefault()!.Operador!,
//        Pos = null,
//        Razon = remito.Razon,
//        Vence = DateTime.Now,
//        EstadoId = _repository.GetEstados().Where(x => x.Name.Equals("ENTREGADO")).FirstOrDefault()!.Id,
//        TipoDocId = _repository.GetTipos().Where(x => x.Name!.Equals("DEVOLUCION")).FirstOrDefault()!.Id,
//        Id = Guid.NewGuid()
//    };

//    foreach (var item in remito.Detalles!)
//    {
//        item.Facturado += devolucion.Find(x => x.Id == item.Id)!.Cantidad;
//    };
//    List<StockProduct> products = new();
//    foreach (BusDetalleDevolucion item in devolucion)
//    {
//        StockProduct? product = _repository.GetProducts().Where(x => x.Id.Equals(item.ProductoId)).Where(x => x.Servicio == false).FirstOrDefault();
//        if (product != null)
//        {
//            product.Cantidad += item.Cantidad;
//            products.Add(product);
//        }
//        item.OperacionId = (Guid)nuevaDevolucion.Id;
//        item.Id = Guid.NewGuid();
//        item.Facturado = item.Cantidad;
//    };

//    var devolucionInsert = _mapper.Map<BusOperacion>(nuevaDevolucion);
//    _repository.Insert(devolucionInsert);
//    _repository.InsertDetalles(_mapper.Map<List<BusOperacionDetalle>>(devolucion));
//    _repository.UpdateIndexs(index);
//    _repository.UpdateDetalles(_mapper.Map<List<BusOperacionDetalle>>(remito.Detalles));
//    _repository.UpdateProducts(products);
//    _repository.Save();
//    return await Task.FromResult(GetOperacion(nuevaDevolucion.Id.ToString()!));
//}
//public List<BusOperacionesDto> RemitosPendientes()
//{
//    List<BusOperacion>? remitos = _repository.Get()
//                .OrderBy(x => x.Cliente.Razon)
//                .Where(x => x.Estado.Name.Equals("ENTREGADO"))
//                .Where(x => x.TipoDoc.Code!.Equals("X"))
//                .Where(x => x.Cliente.Cui != "0")
//                .Union(_repository.Get()
//                .Where(x => x.Estado.Name.Equals("ENTREGADO"))
//                .Where(x => x.TipoDoc.Code!.Equals("X"))
//                .Where(x => x.Cliente.Cui.Equals("0") & DateTime.Now.Date.Subtract((x.Fecha).Date).Days <= 3))
//                .ToList();
//    List<BusOperacionesDto>? dto = _mapper.Map<List<BusOperacion>, List<BusOperacionesDto>>(remitos);
//    List<BusOperacionesDto>? dtoEnCero = new();
//    IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
//    foreach (BusOperacionesDto? item in dto)
//    {
//        if (item.Total.Equals(0.0m))
//        {
//            dtoEnCero.Add(item);
//        }
//        else
//        {
//            item.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
//            item.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
//            item.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
//            item.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
//            item.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
//            item.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
//            item.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
//        }
//    }
//    foreach (BusOperacionesDto? item in dtoEnCero)
//    {
//        dto.Remove(item);
//    }
//    return dto;
//}

//public List<BusDevolucionesDto> Devoluciones()
//{
//    List<BusOperacion>? devoluciones = _repository.Get()
//                .OrderBy(x => x.Cliente.Razon)
//                .Where(x => !x.Estado.Name.Equals("PAGADO"))
//                .Where(x => x.TipoDoc.Code!.Equals("D"))
//                .ToList();
//    List<BusDevolucionesDto>? dto = _mapper.Map<List<BusDevolucionesDto>>(devoluciones);
//    IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
//    foreach (BusDevolucionesDto? item in dto)
//    {

//        item.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
//        item.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
//        item.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
//        item.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
//        item.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
//        item.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
//        item.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
//    }
//    return dto;
//}
//#endregion

//#region Presupuestos
//public BusOperacionesDto NuevaOperacion(string operador)
//{
//    SystemIndex? index = _repository.GetIndexs();
//    BusOperacionesInsert busoperacionesinsert = new()
//    {
//        Id = Guid.NewGuid(),
//        Operador = operador,
//        CodAut = "",
//        ClienteId = _opClientes.Get().Where(x => x.Cui == "0").First().Id.ToString(),
//        TipoDocId = _repository.GetTipos().Where(x => x.Name == "PRESUPUESTO").First().Id,
//        EstadoId = _repository.GetEstados().Where(x => x.Name == "ABIERTO").First().Id,
//        Fecha = DateTime.Now,
//        Numero = index.Presupuesto += 1,
//        Pos = 0,
//        Vence = DateTime.Now,
//        Razon = _opClientes.Get().Where(x => x.Cui == "0").First().Razon
//    };

//    _repository.Insert(_mapper.Map<BusOperacionesInsert, BusOperacion>(busoperacionesinsert));
//    _repository.UpdateIndexs(index);
//    _repository.Save();
//    return GetOperacion(busoperacionesinsert.Id.ToString()!);
//}
//public List<BusOperacionesDto> Presupuestos()
//{
//    List<BusOperacion>? presupuestos = _repository.Get()
//                .OrderBy(x => x.Cliente.Razon)
//                .Where(x => x.Estado.Name.Equals("ABIERTO"))
//                .Where(x => x.TipoDoc.Code!.Equals("P"))
//                .Where(x => DateTime.Now.Date.Subtract((x.Fecha).Date).Days <= 15)
//                .ToList();
//    List<BusOperacionesDto>? dto = _mapper.Map<List<BusOperacion>, List<BusOperacionesDto>>(presupuestos);
//    IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
//    foreach (BusOperacionesDto? item in dto)
//    {
//        item.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
//        item.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
//        item.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
//        item.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
//        item.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
//        item.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
//        item.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
//    }
//    return dto;
//}

//#endregion
//#region Ordenes
//public BusOrdenesTicketDto NuevaOrden(string id)
//{
//    if (!(TipoOperacion(id).Equals("P")
//      &&
//      OperacionEstado(id, "ABIERTO")))
//    {
//        throw new Exception("Este documento no puede pasar a Orden");
//    }
//    BusOperacion? operacion = _repository.Get(id);
//    if (operacion.Cliente.Cui.Equals("0")) throw new Exception("No se pueden generar órdenes a las Ventas Minoritas");
//    SystemIndex? index = _repository.GetIndexs();
//    operacion.Numero = index.Orden += 1;
//    operacion.TipoDoc = _repository.GetTipos().Where(x => x.Code!.Equals("O")).FirstOrDefault()!;
//    operacion.Fecha = DateTime.Now;
//    _repository.UpdateIndexs(index);
//    _repository.Update(operacion);
//    _repository.Save();
//    return _mapper.Map<BusOperacion, BusOrdenesTicketDto>(operacion);
//}
//public List<BusOperacionesDto> OrdenesByEstado(string estado)
//{
//    List<BusOperacion>? ordenes = _repository.Get()
//                .OrderBy(x => x.Cliente.Razon)
//                .Where(x => x.Estado.Id.Equals(Guid.Parse(estado)))
//                .Where(x => x.TipoDoc.Code!.Equals("O"))
//                .ToList();
//    List<BusOperacionesDto>? dto = _mapper.Map<List<BusOperacion>, List<BusOperacionesDto>>(ordenes);
//    IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
//    foreach (BusOperacionesDto? item in dto)
//    {
//        item.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
//        item.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
//        item.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
//        item.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
//        item.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
//        item.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
//        item.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
//    }
//    return dto;
//}
//#endregion

//#region auxiliares
//public List<BusOperacionTipoDto> TipoOperacions()
//{
//    List<BusOperacionTipo> tipos = _repository.GetTipos();
//    return _mapper.Map<List<BusOperacionTipo>, List<BusOperacionTipoDto>>(tipos);
//}

//public List<BusEstadoDto> Estados()
//{
//    List<BusEstado> estados = _repository.GetEstados();
//    return _mapper.Map<List<BusEstado>, List<BusEstadoDto>>(estados);
//}

//#endregion