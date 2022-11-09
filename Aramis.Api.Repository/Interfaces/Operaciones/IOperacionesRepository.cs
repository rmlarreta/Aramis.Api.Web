using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Operaciones
{
    public interface IOperacionesRepository
    {
        void Insert(BusOperacion entity);
        void Update(BusOperacion entity);
        BusOperacion Get(string id);
        void DeleteDetalles(List<BusOperacionDetalle> detalles);
        void DeleteOperacion(string operacion);
        List<BusEstado> GetEstados();
        List<StockProduct> GetProducts();
        void UpdateProducts(List<StockProduct> products);
        SystemIndex GetIndexs();
        void UpdateIndexs(SystemIndex indexs); 
        bool Save();
    }
}
