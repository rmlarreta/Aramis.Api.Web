using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Stock
{
    public interface IStockRepository
    {
        StockProduct GetProduct(string id);
        IEnumerable<StockProduct> GetProducts();
        bool Update(StockProduct product);
        bool Delete(string id);
        bool Insert(StockProduct product);

        StockRubro GetRubro(string id);
        IEnumerable<StockRubro> GetRubroList();
        bool UpdateRubro(StockRubro rubro);
        bool DeleteRubro(string id);
        bool InsertRubro(StockRubro rubro);

        StockIva GetIva(string id);
        IEnumerable<StockIva> GetIvas();

    }
}
