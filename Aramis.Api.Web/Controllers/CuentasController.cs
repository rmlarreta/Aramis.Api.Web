using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.ExceptionService.Interfaces;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{

    public class CuentasController : CommonController
    {
        private readonly ICuentasService _cuentasService;
        private readonly IMovimientosCuentasService _movimientosCuentasService;
        private readonly ISecurityService _securityService;
        public CuentasController(IExceptionService exceptionService, ICuentasService cuentasService, ISecurityService securityService, IMovimientosCuentasService movimientosCuentasService) : base(exceptionService)
        {
            _cuentasService = cuentasService;
            _movimientosCuentasService = movimientosCuentasService;
            _securityService = securityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_cuentasService.GetAllCuentas());
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> MovimientoCuentas([FromBody] CobCuentaMovimientoDto movimiento)
        {
            try
            {
                movimiento.Operador = _securityService.GetUserAuthenticated();
                movimiento.Fecha = DateTime.Now;
                await _movimientosCuentasService.Insert(movimiento);
                return Ok("Movimiento Ingresado Correctamente");
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }
    }
}
