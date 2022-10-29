using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.Repository.Models;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ISecurityService _securityService;
        public UsersController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Authenticate(string user, string password)
        {
            try
            {
                SecurityService.ModelsDto.UserAuth? data = _securityService.Authenticate(user, password);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Insert([FromBody] UserInsertDto userInsertDto)
        {
            try
            {
                SecUser user = new()
                {
                    UserName = userInsertDto.UserName!,
                    RealName = userInsertDto.RealName!,
                    Id = Guid.NewGuid(),
                };
                _securityService.CreateUser(user, userInsertDto.PassWord!);
                return Ok("Usuario Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
