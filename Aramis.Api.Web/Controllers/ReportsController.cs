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
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }
    }
}
