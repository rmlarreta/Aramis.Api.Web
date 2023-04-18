using Aramis.Api.Commons.ModelsDto.Suppliers;
using Aramis.Api.SupplierService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ProveedoresController : ControllerBase
    {
        private readonly ISuppliers _suppliers;

        public ProveedoresController(ISuppliers suppliers)
        {
            _suppliers = suppliers;
        }

        [HttpPost]
        public IActionResult Add([FromBody] OpDocumentoProveedorDto documento)
        {
            try
            {
                return Ok(_suppliers.InsertDocument(documento));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException != null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var data = _suppliers.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException != null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetByProveedor(string id)
        {
            try
            {
                var data = _suppliers.GetByProveedor(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException != null ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetByEstado(string id)
        {
            try
            {
                var data = _suppliers.GetByState(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException != null ? ex.InnerException.Message : ex.Message });
            }
        } 

    }
}
