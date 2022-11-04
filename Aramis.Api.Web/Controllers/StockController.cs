using Aramis.Api.Commons.ModelsDto.Stock;
using Aramis.Api.Repository.Models;
using Aramis.Api.StockService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase   
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService=stockService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ProductsList()
        {
            try
            {
                var data = _stockService.GetList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet] 
        public IActionResult ProductById(string id)
        {
            try
            {
                var data = _stockService.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ProductAdd([FromBody] StockProductInsert productDto)
        {
            try
            {
                _stockService.Insert(productDto);
                return Ok("Producto Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPatch]
        public IActionResult ProductUpdate([FromBody] StockProductInsert productDto)
        {
            try
            {
                _stockService.Update(productDto);
                return Ok("Producto Modificado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult ProductDelete(string id)
        {
            try
            {
                _stockService.Delete(id);
                return Ok("Producto Eliminado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        public IActionResult RubroById(string id)
        {
            try
            {
                var data = _stockService.GetRubro(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Rubros()
        {
            try
            {
                var data = _stockService.GetRubroList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpPost]
        public IActionResult RubroInsert([FromBody] StockRubro rubro)
        {
            try
            {
                _stockService.InsertRubro(rubro);
                return Ok("Rubro Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        } 

        [HttpPatch]
        public IActionResult RubroUpdate([FromBody] StockRubro rubro)
        {
            try
            {
                _stockService.UpdateRubro(rubro);
                return Ok("Rubro Modificado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Ivas()
        {
            try
            {
                var data = _stockService.GetIvaList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }

        [HttpGet]
        public IActionResult IvaById(string id)
        {
            try
            {
                var data = _stockService.GetIva(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException!.Message.Any() ? ex.InnerException.Message : ex.Message });
            }
        }


    }
}
