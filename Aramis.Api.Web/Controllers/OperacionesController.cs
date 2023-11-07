using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.Commons.ModelsDto.Ordenes;
using Aramis.Api.ExceptionService.Interfaces;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.SecurityService.Extensions;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
   
    public class OperacionesController : CommonController
    {
        private readonly IOperacionesService _operacionesService;
        private readonly ISecurityService _securityService;       
        public OperacionesController(IExceptionService exceptionService, IOperacionesService operacionesService, ISecurityService securityService) : base(exceptionService)
        {
            _operacionesService = operacionesService;
            _securityService = securityService;
        }

        [HttpGet]
        public async Task<IActionResult> NuevaOperacion()
        {
            try
            {
                var operador = _securityService.GetUserAuthenticated();
                BusOperacionesDto data = await _operacionesService.NuevaOperacion(null,operador);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> NuevoRemito(string id)
        {
            try
            {
                BusOperacionesDto? data = await _operacionesService.NuevoRemito(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPost] 
        public async Task<IActionResult> NuevaDevolucion(List<BusDetalleDevolucion> devolucion)
        {
            try
            {
                foreach(var item in devolucion)
                {
                    item.Operador = _securityService.GetUserAuthenticated();
                }
                BusOperacionesDto? data = await _operacionesService.NuevaDevolucion(devolucion);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult NuevaOrden(string id)
        {
            try
            {
                BusOrdenesTicketDto? data = _operacionesService.NuevaOrden(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
            }

        }

        [HttpGet]
        public IActionResult Devoluciones()
        {
            try
            {
                List<BusDevolucionesDto>? data = _operacionesService.Devoluciones();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
            }

        }

        [HttpPost]
        public IActionResult InsertDetalle([FromBody] List<BusDetalleOperacionesInsert> detalle)
        {
            try
            {
                BusOperacionesDto? data = _operacionesService.InsertDetalle(detalle);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPatch]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult UpdateDetalle([FromBody] BusDetalleOperacionesInsert detalle)
        {
            try
            {
                BusOperacionesDto? data = _operacionesService.UpdateDetalle(detalle);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
            }
        }

        #endregion
    }
}
