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
                UserAuth? data = _securityService.Authenticate(user, password);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ChangePassword(string user, string password,string npassword)
        {
            try
            {
                UserAuth? data = _securityService.ChangePassword(user, password, npassword);
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
        
        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetUsers()
        {
            try
            {
                var data = _securityService.GetAllUsers();                
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetUserById(string id)
        {
            try
            {
                var data = _securityService.GetUserById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetUserByName(string name)
        {
            try
            {
                var data = _securityService.GetUserByName(name);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
       
        [HttpPatch]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult Update([FromBody] UserDto userDto)
        {
            try
            { 
                _securityService.UpdateUser(userDto);
                return Ok("Usuario Actualizado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                _securityService.DeleteUser(id);
                return Ok("Usuario ELiminado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
