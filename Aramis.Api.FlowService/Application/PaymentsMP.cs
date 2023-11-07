using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Aramis.Api.FlowService.Application
{
    public class PaymentsMP : IPaymentsMp
    {
        private readonly IService<CobPo> _points;
        private readonly IService<SystemIndex> _indexs;
        public PaymentsMP(IService<CobPo> points, IService<SystemIndex> indexs)
        {
            _points = points;
            _indexs = indexs;
        }
        public async Task<PaymentIntentResponseDto> CreatePaymentIntent(PaymentIntentDto PaymentIntent, string PosId)
        {
            try
            {
                await Getpaymentintentlist(PosId);
                CobPo? point = _points.Get(Guid.Parse(PosId));
                if (point == null)
                {
                    throw new Exception("Verifique, no existen dispositivos asociados");
                }
                using (HttpClient? httpClient = new())
                {
                    using HttpRequestMessage? request = new(new HttpMethod("POST"), $"https://api.mercadopago.com/point/integration-api/devices/{point.DeviceId}/payment-intents");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {point.Token}");
                    if (!_indexs.Get().First().Production) request.Headers.TryAddWithoutValidation("x-test-scope", "sandbox"); //borrar en produccion
                    request.Content = JsonContent.Create(PaymentIntent);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    using HttpResponseMessage? response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            PaymentIntentResponseDto? result = await response.Content.ReadFromJsonAsync<PaymentIntentResponseDto>();
                            return result!;

                        }
                        catch (NotSupportedException) // When content type is not valid
                        {
                            Console.WriteLine("The content type is not supported.");
                        }
                        catch (JsonException) // Invalid JSON
                        {
                            Console.WriteLine("Invalid JSON.");
                        }
                    }
                }
                return null!;
            }

            catch (Exception ex)
            {

                throw new Exception(ex.InnerException is not null ? ex.InnerException.Message : ex.Message);
            }
        }
        public async Task Getpaymentintentlist(string id)
        {
            try
            {
                CobPo? point = _points.Get(Guid.Parse(id));
                if (point == null)
                {
                    throw new Exception("Verifique, no existen dispositivos asociados");
                }
                using HttpClient? httpClient = new();
                using HttpRequestMessage? request = new(new HttpMethod("GET"), $"https://api.mercadopago.com/point/integration-api/payment-intents/events?startDate={DateTime.Today:yyyy-MM-dd}&endDate={DateTime.Today:yyyy-MM-dd}");
                request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {point.Token}");
                if (!_indexs.Get().First().Production) request.Headers.TryAddWithoutValidation("x-test-scope", "sandbox"); //borrar en produccion
                using HttpResponseMessage? response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        EventoDto? events = await response.Content.ReadFromJsonAsync<EventoDto>();
                        foreach (Evento? evento in events!.Events!)
                        {
                            if (evento.Status == "OPEN")
                            {
                                await CancelPaymentIntent(evento.Payment_intent_id!, id);
                            }
                        }

                    }
                    catch (NotSupportedException) // When content type is not valid
                    {
                        Console.WriteLine("The content type is not supported.");
                    }
                    catch (JsonException) // Invalid JSON
                    {
                        Console.WriteLine("Invalid JSON.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException is not null ? ex.InnerException.Message : ex.Message);
            }
        }
        public async Task<CancelIntentPayDto> CancelPaymentIntent(string paymentIntent, string id)
        {
            try
            {
                CobPo? point = _points.Get(Guid.Parse(id));
                if (point == null)
                {
                    throw new Exception("Verifique, no existen dispositivos asociados");
                }
                using (HttpClient? httpClient = new())
                {
                    using HttpRequestMessage? request = new(new HttpMethod("DELETE"), $"https://api.mercadopago.com/point/integration-api/devices/{point.DeviceId}/payment-intents/{paymentIntent}");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {point.Token}");
                    if (!_indexs.Get().First().Production) request.Headers.TryAddWithoutValidation("x-test-scope", "sandbox"); //borrar en produccion
                    using HttpResponseMessage? response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            CancelIntentPayDto? result = await response.Content.ReadFromJsonAsync<CancelIntentPayDto>();
                            return result!;
                        }
                        catch (NotSupportedException) // When content type is not valid
                        {
                            Console.WriteLine("The content type is not supported.");
                        }
                        catch (JsonException) // Invalid JSON
                        {
                            Console.WriteLine("Invalid JSON.");
                        }
                    }
                }
                return null!;
            }

            catch (Exception ex)
            {

                throw new Exception(ex.InnerException is not null ? ex.InnerException.Message : ex.Message);
            }
        }
        public async Task<StateIntentPayDto> StatePaymentIntent(string paymentIntentId, string id)
        {
            try
            {
                CobPo? point = _points.Get(Guid.Parse(id));
                if (point == null)
                {
                    throw new Exception("Verifique, no existen dispositivos asociados");
                }
                using (HttpClient? httpClient = new())
                {
                    using HttpRequestMessage? request = new(new HttpMethod("GET"), $"https://api.mercadopago.com/point/integration-api/payment-intents/{paymentIntentId}/events");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {point.Token}");
                    if (!_indexs.Get().First().Production) request.Headers.TryAddWithoutValidation("x-test-scope", "sandbox"); //borrar en produccion
                    using HttpResponseMessage? response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            StateIntentPayDto? result = await response.Content.ReadFromJsonAsync<StateIntentPayDto>();
                            return result!;

                        }
                        catch (NotSupportedException) // When content type is not valid
                        {
                            Console.WriteLine("The content type is not supported.");
                        }
                        catch (JsonException) // Invalid JSON
                        {
                            Console.WriteLine("Invalid JSON.");
                        }
                    }
                }
                return null!;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.InnerException is not null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
