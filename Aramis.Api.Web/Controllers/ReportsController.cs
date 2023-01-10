using Aramis.Api.ReportService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _service;
        public ReportsController(IReportsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ReporteFactura(string id)
        {
            try
            {
                return _service.Operaciones.FacturaReport(id);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ReporteRemito(string id)
        {
            try
            {
                return _service.Operaciones.RemitoReport(id);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ReportePresupuesto(string id)
        {
            try
            {
                return _service.Operaciones.PresupuestoReport(id);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ReporteRecibo(string id)
        {
            try
            {
                return _service.Operaciones.RecibosReport(id);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }

        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult ReporteTicketOrden(string id)
        {
            try
            {
                return _service.Operaciones.TicketOrdenReport(id);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }
    }
}
