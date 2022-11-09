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
        private readonly ISecurityService _securityService;
        public PagosController(IRecibosService recibosService, ISecurityService securityService, IPagosService pagosService)
        {
            _recibosService = recibosService;
            _securityService = securityService;
            _pagosService = pagosService;
        }

        [HttpPost]
        public IActionResult InsertRecibo([FromBody] ReciboInsert recibo)
        {
            try
            {
                recibo.Operador = _securityService.GetUserAuthenticated();
                recibo.Fecha = DateTime.Now;  
                return Ok(_recibosService.InsertRecibo(recibo));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ImputarPago([FromBody] PagoInsert pago)
        {
            try
            { 
                return Ok(_pagosService.NuevoPago(pago));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

    }
}
