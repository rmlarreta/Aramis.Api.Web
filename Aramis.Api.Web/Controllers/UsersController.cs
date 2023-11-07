using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.ExceptionService.Interfaces;
using Aramis.Api.SecurityService.Extensions;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    public class UsersController : CommonController
    {
        private readonly ISecurityService _securityService;
        public UsersController(IExceptionService exceptionService, ISecurityService securityService) : base(exceptionService)
        {
            _securityService = securityService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{user}/{password}")]
        public async Task<IActionResult> AuthenticateAsync(string user, string password)
        {
            try
            {
                UserAuth? data = await _securityService.Authenticate(user, password);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{user}/{password}/{npassword}")]
        public async Task<IActionResult> ChangePassword(string user, string password, string npassword)
        {
            try
            {
                UserAuth? data = await _securityService.ChangePassword(user, password, npassword);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] UserInsertDto userInsertDto)
        {
            try
            {
                UserAuth? data = await _securityService.CreateUser(userInsertDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }

        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetUsers()
        {
            try
            {
                IEnumerable<UserAuth>? data = _securityService.GetAllUsers();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }

        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                UserAuth? data = await _securityService.GetUserById(Guid.Parse(id));
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        [Route("{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            try
            {
                UserAuth? data = await _securityService.GetUserByName(name);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPatch]
        [Authorize(Policy = Policies.Admin)]
        public async Task<IActionResult> Update([FromBody] UserBaseDto userDto)
        {
            try
            {
                await _securityService.UpdateUser(userDto);
                return Ok("USuario Actualizado");
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpDelete]
        [Authorize(Policy = Policies.Admin)]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _securityService.DeleteUser(Guid.Parse(id));
                return Ok("Usuario ELiminado Correctamente");
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }
    }
}
