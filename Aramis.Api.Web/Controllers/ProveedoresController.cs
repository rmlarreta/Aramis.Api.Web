using Aramis.Api.Commons.ModelsDto.Suppliers;
using Aramis.Api.SecurityService.Interfaces;
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
        private readonly ISecurityService _securityService;

        public ProveedoresController(ISuppliers suppliers, ISecurityService securityService)
        {
            _suppliers = suppliers;
            _securityService = securityService;
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
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPatch]
        public IActionResult PagarDocumento([FromBody] OpDocumentProveedorPago documento)
        {
            try
            {
                documento.Operador = _securityService.GetUserAuthenticated();
                return Ok(_suppliers.PagarDocumento(documento));
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
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
                return _exceptionService.ReturnResult(ex);
            }
        }

    }
}
