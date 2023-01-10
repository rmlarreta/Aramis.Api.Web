using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.Repository.Models;
using Aramis.Api.SecurityService.Extensions;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        public UsersController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{user}/{password}")]
        public IActionResult Authenticate(string user, string password)
        {
            try
            {
                UserAuth? data = _securityService.Authenticate(user, password);
                return Ok(data);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{user}/{password}/{npassword}")]
        public IActionResult ChangePassword(string user, string password, string npassword)
        {
            try
            {
                UserAuth? data = _securityService.ChangePassword(user, password, npassword);
                return Ok(data);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
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
                UserDto? data = _securityService.CreateUser(user, userInsertDto.PassWord!);
                return Ok(data);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }

        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetUsers()
        {
            try
            {
                IEnumerable<UserDto>? data = _securityService.GetAllUsers();
                return Ok(data);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }

        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        [Route("{id}")]
        public IActionResult GetUserById(string id)
        {
            try
            {
                UserDto? data = _securityService.GetUserById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        [Route("{name}")]
        public IActionResult GetUserByName(string name)
        {
            try
            {
                UserDto? data = _securityService.GetUserByName(name);
                return Ok(data);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPatch]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult Update([FromBody] UserDto userDto)
        {
            try
            {
                UserDto? data = _securityService.UpdateUser(userDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpDelete]
        [Authorize(Policy = Policies.Admin)]
        [Route("{id}")]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                _securityService.DeleteUser(id);
                return Ok("Usuario ELiminado Correctamente");
            }
            catch (Exception ex)
            {
              return BadRequest(new { message = ex.InnerException!=null ? ex.InnerException.Message : ex.Message });
            }
        }
    }
}
