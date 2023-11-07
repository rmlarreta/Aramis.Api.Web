using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.ExceptionService.Interfaces;
using Aramis.Api.FiscalService.Interfaces;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{

    public class FiscalController : CommonController
    {
        private readonly IFiscalService _fiscalService;
        private readonly ISecurityService _securityService;

        public FiscalController(IExceptionService exceptionService, IFiscalService fiscalService, ISecurityService securityService) : base(exceptionService)
        {
            _securityService = securityService;
            _fiscalService = fiscalService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerarFacturaAsync([FromBody] List<BusDetalleOperacionesInsert> busDetalles)
        {
            try
            {
                foreach (BusDetalleOperacionesInsert? det in busDetalles)
                {
                    det.Operador = _securityService.GetUserAuthenticated();
                }

                BusOperacionesDto data = await _fiscalService.GenerarFactura(busDetalles);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }
    }
}
