using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.FiscalService.Interfaces;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FiscalController : ControllerBase
    {
        private readonly IFiscalService _fiscalService;
        private readonly ISecurityService _securityService;
        public FiscalController(IFiscalService fiscalService, ISecurityService securityService)
        {
            _securityService = securityService;
            _fiscalService = fiscalService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerarFacturaAsync([FromBody] List<BusDetallesOperacionesDto> busDetalles)
        {
            try
            {
                foreach (BusDetallesOperacionesDto? det in busDetalles)
                {
                    det.Operador = _securityService.GetUserAuthenticated();
                }

                BusOperacionesDto data = await _fiscalService.GenerarFactura(busDetalles);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }
    }
}
