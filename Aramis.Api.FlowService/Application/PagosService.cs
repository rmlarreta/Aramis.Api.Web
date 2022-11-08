
using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces; 
using Aramis.Api.Repository.Interfaces.Pagos;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FlowService.Application
{
    public class PagosService : IPagosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PagosService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
        }
        public bool NuevoPago(PagoInsert pago)
        { 
            var recibo = _unitOfWork.Recibos.Get(pago.ReciboId.ToString());  
            if (recibo.CobReciboDetalles.Sum(x => x.Monto) == 0) throw new Exception("No puede insertarse un Recibo en 0");
            foreach(var item in recibo!.CobReciboDetalles)
            {  
                CobCuentum? cuenta = _unitOfWork.Cuentas.Get().FirstOrDefault(x=>x.Id.Equals(_unitOfWork.TipoPagos.Get().FirstOrDefault(x => x.Id.Equals(item.Tipo))!.CuentaId));
                if (cuenta is null) throw new Exception("Existe un error en las cuentas");
                cuenta.Saldo += item.Monto;
                _unitOfWork.Cuentas.Update(cuenta);
            }
            if (_unitOfWork.OperacionPagos.Get().Where(x => x.ReciboId.Equals(pago.ReciboId)).Any()) throw new Exception("Ese Recibo ya ha sido imputado");
            List<BusOperacion> ops = new();
            foreach (var id in pago.Operaciones)
            {
                var op= _unitOfWork.Operaciones.Get(id);
                ops.Add(op!);
            } 
            var operaciones = _mapper.Map<List<BusOperacion>, List<BusOperacionesDto>>(ops);
            if (!recibo.CobReciboDetalles.Sum(x => x.Monto).Equals(operaciones.Sum(x =>x.Total))) throw new Exception("Existe una diferencia en los pagos ingresados");
            foreach(var item in ops)
            {
                item.EstadoId = _unitOfWork.Estados.Get().Where(x => x.Name.Equals("PAGADO")).SingleOrDefault()!.Id;
                _unitOfWork.Operaciones.Update(item);
                BusOperacionPago op = new()
                {
                    Id = Guid.NewGuid(),
                    OperacionId = item.Id,
                    ReciboId = pago.ReciboId
                };
                _unitOfWork.OperacionPagos.Add(op);
            }             
            return _unitOfWork.Save();
        }
    }
}
