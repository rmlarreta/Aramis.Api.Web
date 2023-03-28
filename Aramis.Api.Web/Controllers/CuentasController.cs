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
    public class CuentasController : ControllerBase
    {
        private readonly ICuentasService _cuentasService;
        private readonly ISecurityService _securityService;

        public CuentasController(ICuentasService cuentasService, ISecurityService securityService)
        {
            _cuentasService = cuentasService;
            _securityService = securityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<CobCuentDto> data = _cuentasService.GetAll();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult MovimientoCuentas([FromBody] CobCuentaMovimientoDto movimiento)
        {
            movimiento.Operador = _securityService.GetUserAuthenticated();
            movimiento.Fecha = DateTime.Now;
            CobCuentDto data = _cuentasService.Insert(movimiento);
            return Ok(data);
        }
    }
}
