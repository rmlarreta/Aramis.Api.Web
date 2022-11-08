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
        public IActionResult NuevaOperacion([FromBody] BusOperacionesInsert op)
        {
            try
            {
                op.Operador = _securityService.GetUserAuthenticated();
                op.Fecha = DateTime.Now;
                BusOperacionesDto data = _operacionesService.NuevaOperacion(op);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }
        
        [HttpPost]
        public IActionResult UpdateOperacion([FromBody] BusOperacionesInsert op)
        {
            try
            {
                op.Operador = _securityService.GetUserAuthenticated();
                BusOperacionesDto data = _operacionesService.UpdateOperacion(op);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpDelete]
        public bool DeleteOperacion(string id)
        {
            return _operacionesService.DeleteOperacion(id);
        }

        [HttpGet]
        public BusOperacionesDto GetOperationById(string id)
        {
            return _operacionesService.GetOperacion(id);
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

        [HttpDelete]
        public BusOperacionesDto DeleteDetalle(string id)
        {
            return _operacionesService.DeleteDetalle(id);
        }

        [HttpPatch]
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

        [HttpPost]
        public IActionResult InsertObservacion([FromBody] BusObservacionesInsert observa)
        {
            try
            {
                observa.Operador = _securityService.GetUserAuthenticated();
                observa.Fecha = DateTime.Now;
                _operacionesService.InsertObservacion(observa);
                return Ok("Correcto");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult DeleteObservacion(string id)
        {
            try
            {
                _operacionesService.DeleteObservacion(id);
                return Ok("Correcto");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPatch]
        public IActionResult UpdateObservacion([FromBody] BusObservacionesInsert observa)
        {
            try
            {
                observa.Operador = _securityService.GetUserAuthenticated();
                observa.Fecha = DateTime.Now;
                _operacionesService.UpdateObservacion(observa);
                return Ok("Correcto");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }
    }
}
