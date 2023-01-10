using Aramis.Api.Commons.ModelsDto.Stock;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.StockService.Interfaces
{
    public interface IStockService
    {
        StockProductDto Insert(StockProductInsert product);
        bool Delete(string id);
        StockProductDto Update(StockProductInsert product);
        StockProductDto GetById(string id);
        List<StockProductDto> GetList();

        bool InsertRubro(StockRubroDto rubro);
        bool UpdateRubro(StockRubroDto rubro);
        StockRubroDto GetRubro(string id);
        List<StockRubroDto> GetRubroList();

        StockIvaDto GetIva(string id);
        List<StockIvaDto> GetIvaList();
    }
}
