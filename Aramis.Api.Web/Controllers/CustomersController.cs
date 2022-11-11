using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.Repository.Interfaces.Customers;
using Aramis.Api.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;
        private readonly ICustomersAttributesRepository _customersAttributesRepository;
        public CustomersController(ICustomersService customersService, ICustomersAttributesRepository customersAttributesRepository)
        {
            _customersService = customersService;
            _customersAttributesRepository = customersAttributesRepository;
        }

        [HttpPost]
        public IActionResult Insert([FromBody] OpClienteInsert opclientedto)
        {
            try
            {
                OpClienteDto data = _customersService.Insert(opclientedto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPatch]
        public IActionResult Update([FromBody] OpClienteInsert opclientedto)
        {
            try
            {
                OpClienteDto data = _customersService.Update(opclientedto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _customersService.Delete(id);
                return Ok("Cliente ELiminado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        public IEnumerable<OpClienteDto> GetAll()
        {
            return _customersService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public OpClienteDto Get(string id)
        {
            return _customersService.GetById(id);
        }

        #region Attributes
        [HttpGet]
        public IEnumerable<OpResp> GetRespList()
        {
            return _customersAttributesRepository.GetRespList();
        }

        [HttpGet]
        [Route("{id}")]
        public OpResp GetResp(string id)
        {
            return _customersAttributesRepository.GetResp(id);
        }

        [HttpGet]
        public IEnumerable<OpPai> GetPaisList()
        {
            return _customersAttributesRepository.GetPaisList();
        }

        [HttpGet]
        [Route("{id}")]
        public OpPai GetPais(string id)
        {
            return _customersAttributesRepository.GetPais(id);
        }

        [HttpGet]
        public IEnumerable<OpGender> GetGenderList()
        {
            return _customersAttributesRepository.GetGenderList();
        }

        [HttpGet]
        [Route("{id}")]
        public OpGender GetGender(string id)
        {
            return _customersAttributesRepository.GetGender(id);
        }
        #endregion Attributes
    }
}
