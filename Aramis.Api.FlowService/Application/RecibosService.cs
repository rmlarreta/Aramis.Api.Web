using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Interfaces.Recibos;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FlowService.Application
{
    public class RecibosService : IRecibosService
    {
        private readonly IPaymentsMp _paymentsMP;
        private readonly IRecibosRepository _recibos;
        private readonly IMapper _mapper;
        public RecibosService(IPaymentsMp paymentsMP, IRecibosRepository recibos, IMapper mapper)
        {
            _paymentsMP = paymentsMP;
            _recibos = recibos;
            _mapper = mapper;
        }
        public ReciboInsert InsertRecibo(ReciboInsert recibo)
        {
            SystemIndex? index = _recibos.GetIndexs();
            recibo.Numero = index.Recibo += 1; 
            _recibos.UpdateIndexs(index);
            _recibos.Add(_mapper.Map<ReciboInsert, CobRecibo>(recibo));
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
