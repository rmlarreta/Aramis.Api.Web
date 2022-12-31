
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
        private readonly IPagosRepository _repository;
        private readonly IMapper _mapper;
        public PagosService(IPagosRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> NuevoPago(PagoInsert pago)
        { 
            CobRecibo? recibo = await Task.FromResult(_repository.Recibos.Get(pago.ReciboId.ToString()));
            if (recibo.CobReciboDetalles.Sum(x => x.Monto) == 0)
            {
                throw new Exception("No puede insertarse un Recibo en 0");
            }

            foreach (CobReciboDetalle? item in recibo!.CobReciboDetalles)
            {
                CobCuentum? cuenta = _repository.Cuentas.Get().FirstOrDefault(x => x.Id.Equals(_repository.TipoPagos.Get().FirstOrDefault(x => x.Id.Equals(item.Tipo))!.CuentaId));
                if (cuenta is null)
                {
                    throw new Exception("Existe un error en las cuentas");
                }

                cuenta.Saldo += item.Monto;
                _repository.Cuentas.Update(cuenta);
            }
            if (_repository.OperacionPagos.Get().Where(x => x.ReciboId.Equals(pago.ReciboId)).Any())
            {
                throw new Exception("Ese Recibo ya ha sido imputado");
            }

            List<BusOperacion> ops = new();
            foreach (string? id in pago.Operaciones)
            {
                BusOperacion? op = _repository.Operaciones.Get(id);
                ops.Add(op!);
            }
            List<BusOperacionesDto>? operaciones = _mapper.Map<List<BusOperacion>, List<BusOperacionesDto>>(ops);
            if (!recibo.CobReciboDetalles.Sum(x => Math.Round(x.Monto,2)).Equals(operaciones.Sum(x => Math.Round(x.Total,2))))
            {
                throw new Exception("Existe una diferencia en los pagos ingresados");
            } 

            foreach (BusOperacion? item in ops)
            {
                item.Estado = _repository.Estados.Get().Where(x => x.Name.Equals("PAGADO")).SingleOrDefault()!;
                 _repository.Operaciones.Update(item);
                BusOperacionPagoDto op = new()
                {
                    Id = Guid.NewGuid(),
                    OperacionId = item.Id,
                    ReciboId =pago.ReciboId
                };
                _repository.OperacionPagos.Add(_mapper.Map<BusOperacionPagoDto, BusOperacionPago>(op));
            }
            return _repository.Save();
        }
    }
}
