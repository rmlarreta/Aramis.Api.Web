using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.SecurityService.Extensions;
using Aramis.Api.SecurityService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize(Policy = Policies.Admin)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        public RolesController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<RoleDto>? data = _securityService.GetAllRoles();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                RoleDto? data = _securityService.GetRoleById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                RoleDto? data = _securityService.GetRoleByName(name);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] RoleDto roleDto)
        {
            try
            {
                _securityService.CreateRole(roleDto);
                return Ok("Rol Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPatch]
        public IActionResult Update([FromBody] RoleDto roleDto)
        {
            try
            {
                _securityService.UpdateRole(roleDto);
                return Ok("Rol Actualizado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string Id)
        {
            try
            {
                _securityService.DeleteRole(Id);
                return Ok("Rol Eliminado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }
    }
}
