using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OperacionesController : ControllerBase
    {
        private readonly IOperacionesService _operacionesService;
        private readonly ISecurityService _securityService;
        public OperacionesController(IOperacionesService operacionesService, ISecurityService securityService)
        {
            _operacionesService = operacionesService;
            _securityService = securityService;
        }

        [HttpPost]
        public IActionResult Insert([FromBody] BusOperacionesInsert op)
        {
            try
            {
                op.Operador = _securityService.GetUserAuthenticated();
                BusOperacionesDto data = _operacionesService.NuevaOperacion(op);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost]
        public IActionResult InsertDetalle([FromBody] BusDetalleOperacionesInsert detalle)
        {
            try
            {
                BusOperacionesDto data = _operacionesService.InsertDetalle(detalle);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        public BusOperacionesDto GetOperationById(string id)
        {
            return _operacionesService.GetOperacion(id);
        }

        [HttpDelete]
        public BusOperacionesDto DeleteDetalle(string id)
        {
            return _operacionesService.DeleteDetalle(id);
        }

        [HttpPost]
        public IActionResult UpdateDetalle([FromBody] BusDetalleOperacionesInsert detalle)
        {
            try
            {
                BusOperacionesDto data = _operacionesService.UpdateDetalle(detalle);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }
    }
}
