using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Interfaces.Pagos;
using Aramis.Api.Repository.Interfaces.Recibos;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FlowService.Application
{
    public class RecibosService : IRecibosService
    {
        private readonly IPaymentsMp _paymentsMP;
        private readonly IPagosRepository _repository;
        private readonly IRecibosRepository _recibos;
        private readonly IMapper _mapper;

        public RecibosService(IPaymentsMp paymentsMP, IPagosRepository repository, IRecibosRepository recibos, IMapper mapper)
        {
            _paymentsMP = paymentsMP;
            _repository = repository;
            _recibos = recibos;
            _mapper = mapper;
        }

        public CobReciboInsert InsertRecibo(CobReciboInsert recibo)
        {
            SystemIndex? index = _recibos.GetIndexs();
            recibo.Numero = index.Recibo += 1;
            _recibos.UpdateIndexs(index);
            foreach (var det in recibo.Detalles!)
            {
                if (det.Observacion == "CUENTA CORRIENTE")
                {
                    det.Cancelado = false;
                }
                CobCuentum? cuenta = _repository.Cuentas.Get().FirstOrDefault(x => x.Id.Equals(_repository.TipoPagos.Get().FirstOrDefault(x => x.Id.Equals(det.Tipo))!.CuentaId));
                if (cuenta is null)
                {
                    throw new Exception("Existe un error en las cuentas");
                }

                cuenta.Saldo += det.Monto;
                _repository.Cuentas.Update(cuenta);
            }
            _recibos.Add(_mapper.Map<CobReciboInsert, CobRecibo>(recibo));
            _recibos.Save();
            return recibo!;
        }

        public async Task<PaymentIntentResponseDto> PagoMP(PaymentIntentDto intent, string PosId)
        {
            PaymentIntentResponseDto? intento = await _paymentsMP.CreatePaymentIntent(intent, PosId)!;
            if (intento == null)
            {
                return null!;
            }

            int intentos = 0;

            while (intentos <= 30)
            {
                StateIntentPayDto? estados = await _paymentsMP.StatePaymentIntent(intento!.Id!, PosId!);
                switch (estados.Status)
                {
                    case "OPEN" or "PROCESSING" or "ON_TERMINAL" or "PROCESSED":
                        intentos += 1;
                        Thread.Sleep(5000);
                        continue;
                    case "CANCELED" or "ERROR":
                        await _paymentsMP.CancelPaymentIntent(intento!.Id!, PosId!);
                        intento.Status = estados.Status;
                        return intento;
                    case "FINISHED":
                        intento.Status = estados.Status;
                        return intento;
                    default:
                        intento.Status = estados.Status;
                        return intento;
                }
            }
            return intento!;
        }
    }
}
