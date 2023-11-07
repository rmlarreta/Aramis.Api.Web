using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.ExceptionService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{

    public class CustomersController : CommonController
    {
        private readonly ICustomersService _customersService;
        private readonly ICustomersAttributes _customersAttributes;
        public CustomersController(IExceptionService exceptionService, ICustomersService customersService, ICustomersAttributes customersAttributes) : base(exceptionService)
        {
            _customersService = customersService;
            _customersAttributes = customersAttributes;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] OpClienteBase opclientedto)
        {
            try
            {
                await _customersService.Insert(opclientedto);
                return Ok("Correcto");
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] OpClienteBase opclientedto)
        {
            try
            {
                await _customersService.Update(opclientedto);
                return Ok("Correcto");
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _customersService.DeleteCliente(Guid.Parse(id));
                return Ok("Cliente ELiminado Correctamente");
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<OpClienteDto>? customers = await _customersService.GetAllClientes();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                OpClienteDto? customer = await _customersService.GetById(Guid.Parse(id));
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Route("{cui}")]
        public async Task<IActionResult> GetByCui(string cui)
        {
            try
            {
                OpClienteDto? customer = await _customersService.GetByCui(cui);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        #region Attributes
        [HttpGet]
        public IActionResult GetRespList()
        {

            try
            {
                return Ok(_customersAttributes.GetRespList());
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetResp(string id)
        {
            try
            {
                return Ok(_customersAttributes.GetResp(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        public IActionResult GetPaisList()
        {
            try
            {
                return Ok(_customersAttributes.GetPaisList());
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPais(string id)
        {
            try
            {
                return Ok(_customersAttributes.GetPais(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        public IActionResult GetGenderList()
        {
            try
            {
                return Ok(_customersAttributes.GetGenderList());
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetGender(string id)
        {
            try
            {
                return Ok(_customersAttributes.GetGender(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }
        #endregion Attributes
    }
}
