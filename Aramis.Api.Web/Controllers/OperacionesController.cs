using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
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
                op.Id = Guid.NewGuid();
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
        [Route("{id}")]
        public IActionResult NuevoRemito(string id)
        {
            try
            {
                BusOperacionesDto? data = _operacionesService.NuevoRemito(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult NuevaOrden(string id)
        {
            try
            {
                Commons.ModelsDto.Ordenes.BusOrdenesTicketDto? data = _operacionesService.NuevaOrden(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Route("{estado}")]
        public IActionResult OrdenesByEstado(string estado)
        {
            try
            {
                List<BusOperacionesDto>? data = _operacionesService.OrdenesByEstado(estado);
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
        [Route("{id}")]
        public IActionResult DeleteOperacion(string id)
        {
            try
            {
                return Ok(_operacionesService.DeleteOperacion(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOperationById(string id)
        {
            try
            {
                BusOperacionesDto? data = _operacionesService.GetOperacion(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }

        }

        [HttpGet]
        public IActionResult RemitosPendientes()
        {
            try
            {
                List<BusOperacionesDto>? data = _operacionesService.RemitosPendientes();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }

        }

        [HttpGet]
        public IActionResult Presupuestos()
        {
            try
            {
                List<BusOperacionesDto>? data = _operacionesService.Presupuestos();
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
                BusOperacionesDto? data = _operacionesService.InsertDetalle(detalle);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteDetalle(string id)
        {
            try
            {
                BusOperacionesDto? data = _operacionesService.DeleteDetalle(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPatch]
        public IActionResult UpdateDetalle([FromBody] BusDetalleOperacionesInsert detalle)
        {
            try
            {
                BusOperacionesDto? data = _operacionesService.UpdateDetalle(detalle);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost]
        public IActionResult InsertObservacion([FromBody] BusObservacionesDto observa)
        {
            try
            {
                observa.Operador = _securityService.GetUserAuthenticated();
                observa.Fecha = DateTime.Now;
                return Ok(_operacionesService.InsertObservacion(observa));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteObservacion(string id)
        {
            try
            {
                return Ok(_operacionesService.DeleteObservacion(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPatch]
        public IActionResult UpdateObservacion([FromBody] BusObservacionesDto observa)
        {
            try
            {
                observa.Operador = _securityService.GetUserAuthenticated();
                observa.Fecha = DateTime.Now;
                return Ok(_operacionesService.UpdateObservacion(observa));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        #region Auxiliares

        [HttpGet]
        public IActionResult Tipos()
        {
            try
            {
                List<BusOperacionTipoDto>? data = _operacionesService.TipoOperacions();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Estados()
        {
            try
            {
                List<BusEstadoDto>? data = _operacionesService.Estados();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        #endregion
    }
}
