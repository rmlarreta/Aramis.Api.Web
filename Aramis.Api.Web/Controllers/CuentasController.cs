using Aramis.Api.FlowService.Interfaces;
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

        public CuentasController(ICuentasService cuentasService)
        {
            _cuentasService = cuentasService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _cuentasService.GetAll();
            return Ok(data);
        }
    }
}
