using Aramis.Api.Commons.ModelsDto.Stock;
using Aramis.Api.SecurityService.Extensions;
using Aramis.Api.StockService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.Web.Controllers
{
    [Authorize(Policy = Policies.Admin)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ProductsList()
        {
            try
            {
                List<StockProductDto>? data = _stockService.GetList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ProductById(string id)
        {
            try
            {
                StockProductDto? data = _stockService.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPost]
        public IActionResult ProductAdd([FromBody] StockProductInsert productDto)
        {
            try
            {
                StockProductDto? data = _stockService.Insert(productDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPatch]
        public IActionResult ProductUpdate([FromBody] StockProductInsert productDto)
        {
            try
            {
                StockProductDto? data = _stockService.Update(productDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPatch]
        public IActionResult ProductsUpdate([FromBody] List<StockProductInsert> productDto)
        {
            try
            {
               _stockService.UpdateRange(productDto);
                return Ok("Operación Correcta");
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult ProductDelete(string id)
        {
            try
            {
                _stockService.Delete(id);
                return Ok("Producto Eliminado Correctamente");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Console.WriteLine("Inner exception: {0}", ex.InnerException);
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult RubroById(string id)
        {
            try
            {
                StockRubroDto? data = _stockService.GetRubro(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        public IActionResult Rubros()
        {
            try
            {
                List<StockRubroDto>? data = _stockService.GetRubroList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPost]
        public IActionResult RubroInsert([FromBody] StockRubroDto rubro)
        {
            try
            {
                _stockService.InsertRubro(rubro);
                return Ok("Rubro Creado Correctamente");
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpPatch]
        public IActionResult RubroUpdate([FromBody] StockRubroDto rubro)
        {
            try
            {
                _stockService.UpdateRubro(rubro);
                return Ok("Rubro Modificado Correctamente");
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        public IActionResult Ivas()
        {
            try
            {
                List<StockIvaDto>? data = _stockService.GetIvaList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult IvaById(string id)
        {
            try
            {
                StockIvaDto? data = _stockService.GetIva(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return _exceptionService.ReturnResult(ex);
            }
        }


    }
}
