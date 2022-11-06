
using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces; 
using Aramis.Api.Repository.Interfaces.Pagos;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.FlowService.Application
{
    public class PagosService : IPagosService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PagosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public bool NuevoPago(PagoInsert pago)
        {
            if (pago.Total == 0) throw new Exception("No puede insertarse un Recibo en 0"); 
            foreach(var item in pago.Detalles)
            {
                CobCuentum? cuenta = _unitOfWork.Cuentas.Get(_unitOfWork.TipoPagos.Get(Guid.Parse(item.Tipo.ToString())).Cuenta!.Id);
                if (cuenta is null) throw new Exception("Existe un error en las cuentas");
                cuenta.Saldo += item.Monto;
                _unitOfWork.Cuentas.Update(cuenta);
            }
            if (!pago.Total.Equals(pago.Operaciones.Sum(x => x.Total))) throw new Exception("Existe una diferencia en los pagos ingresados");
            foreach(var item in pago.Operaciones)
            {
                item.EstadoId = _unitOfWork.Estados.Get().Where(x => x.Name.Equals("PAGADO")).SingleOrDefault()!.Id;
                BusOperacionPago op = new()
                {
                    Id = Guid.NewGuid(),
                    OperacionId = item.Id,
                    ReciboId = pago.ReciboId
                };
            }             
            return _unitOfWork.Save();
        }
    }
}
