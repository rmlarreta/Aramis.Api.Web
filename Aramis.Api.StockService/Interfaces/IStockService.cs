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

        bool InsertRubro(StockRubro rubro);
        bool UpdateRubro(StockRubro rubro);
        StockRubro GetRubro(string id);
        List<StockRubro> GetRubroList();

        StockIva GetIva(string id);
        List<StockIva> GetIvaList();
    }
}
