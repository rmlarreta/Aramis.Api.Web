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
        public string InsertRecibo(ReciboInsert recibo)
        {
            foreach(var det in recibo.Detalles)
            {
                det.ReciboId = recibo.Id;
            }
            _recibos.Add(_mapper.Map<ReciboInsert, CobRecibo>(recibo));
            return recibo.Id.ToString();
        }

        public async Task<PaymentIntentResponseDto> PagoMP(PaymentIntentDto intent, string point)
        { 
           var intento=await _paymentsMP.CreatePaymentIntent(intent, point)!;
           if (intento == null) return null!;
           int intentos = 30;

            while (intentos <=30)
            {
                var estados = await _paymentsMP.StatePaymentIntent(intento!.Id!, point);
                switch (estados.Status)
                {
                    case "OPEN":
                        intentos += 1;
                        Thread.Sleep(2000);
                        continue;
                    case "CANCELED" or "ERROR":
                        await _paymentsMP.CancelPaymentIntent(intento!.Id!, point);
                        intento.Id = string.Empty;
                        return intento; 
                    case "FINISHED":
                        return intento;
                    default: intento.Id = string.Empty; return intento;
                }                
            }
            return intento!;
        } 
    }
}
