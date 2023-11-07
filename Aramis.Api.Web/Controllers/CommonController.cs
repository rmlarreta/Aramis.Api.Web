using Aramis.Api.ExceptionService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public abstract class CommonController : ControllerBase
    {
        protected readonly IExceptionService _exceptionService;

        protected CommonController(IExceptionService exceptionService)
        {
            _exceptionService = exceptionService;
        }
    }
}
