using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IRecibosService _recibosService;
        private readonly IPagosService _pagosService;
        private readonly ITipoPagoService _tipoPagoService; 
        private readonly ISecurityService _securityService;
        public PagosController(IRecibosService recibosService, ISecurityService securityService, IPagosService pagosService, ITipoPagoService tipoPagoService)
        {
            _recibosService = recibosService;
            _securityService = securityService;
            _tipoPagoService = tipoPagoService;
            _pagosService = pagosService;
        }

        [HttpPost]
        public IActionResult InsertRecibo([FromBody] ReciboInsert recibo)
        {
            try
            {
                recibo.Id = Guid.NewGuid();
                recibo.Operador = _securityService.GetUserAuthenticated();
                recibo.Fecha = DateTime.Now;
                foreach(var item in recibo.Detalles!)
                {
                    item.ReciboId=recibo.Id;
                    item.Id = Guid.NewGuid();
                }
                return Ok(_recibosService.InsertRecibo(recibo));
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost]
        [Route("{PosId}")]
        public async Task<IActionResult> CobranzaMPAsync([FromBody] PaymentIntentDto intent, string PosId)
        {
            try
            {
                PaymentIntentResponseDto? data = await _recibosService.PagoMP(intent, PosId);
                return Ok(data);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImputarPago([FromBody] PagoInsert pago)
        {
            try
            {
                return Ok(await _pagosService.NuevoPago(pago));
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }

        #region Auxiliares

        [HttpGet]
        public IActionResult TiposPago()
        {
            var data = _tipoPagoService.GetAll();
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetPos()
        {
            var data = _tipoPagoService.GetPost();
            return Ok(data);
        }
        #endregion
    }
}
