using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.Repository.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public CustomersController(ICustomersService customersService, IMapper mapper)
        {
            _customersService = customersService;
            _mapper = mapper;
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
                return BadRequest(new { message = ex.InnerException != null ? ex.InnerException.Message : ex.Message });
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
                return BadRequest(new { message = ex.InnerException != null ? ex.InnerException.Message : ex.Message });
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
                return BadRequest(new { message = ex.InnerException != null ? ex.InnerException.Message : ex.Message });
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

        [HttpGet]
        [Route("{cui}")]
        public OpClienteDto GetByCui(string cui)
        {
            return _customersService.GetByCui(cui);
        }

        #region Attributes
        [HttpGet]
        public IEnumerable<OpRespDto> GetRespList()
        {
            return _mapper.Map<List<OpResp>, List<OpRespDto>>(_customersService.Attributes.GetRespList());
        }

        [HttpGet]
        [Route("{id}")]
        public OpRespDto GetResp(string id)
        {
            return _mapper.Map<OpResp, OpRespDto>(_customersService.Attributes.GetResp(id));
        }

        [HttpGet]
        public IEnumerable<OpPaiDto> GetPaisList()
        {
            return _mapper.Map<List<OpPai>, List<OpPaiDto>>(_customersService.Attributes.GetPaisList());
        }

        [HttpGet]
        [Route("{id}")]
        public OpPaiDto GetPais(string id)
        {
            return _mapper.Map<OpPai, OpPaiDto>(_customersService.Attributes.GetPais(id));
        }

        [HttpGet]
        public IEnumerable<OpGenderDto> GetGenderList()
        {
            return _mapper.Map<List<OpGender>, List<OpGenderDto>>(_customersService.Attributes.GetGenderList());
        }

        [HttpGet]
        [Route("{id}")]
        public OpGenderDto GetGender(string id)
        {
            return _mapper.Map<OpGender, OpGenderDto>(_customersService.Attributes.GetGender(id));
        }
        #endregion Attributes
    }
}
