using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Operaciones
{
    public interface IOperacionesRepository
    {
        void Insert(BusOperacion entity);
        void Update(BusOperacion entity);
        BusOperacion Get(string id);
        List<BusOperacion> Get();
        List<BusOperacion> GetImpagasByClienteId(string clienteId);
        void DeleteDetalles(List<BusOperacionDetalle> detalles);
        void DeleteOperacion(string operacion);
        void InsertObservaciones(List<BusOperacionObservacion> observaciones);
        void InsertDetalles(List<BusOperacionDetalle> detalles);
        void UpdateDetalles(List<BusOperacionDetalle> detalles);
        List<BusEstado> GetEstados();
        List<BusOperacionTipo> GetTipos();
        List<StockProduct> GetProducts();
        void UpdateProducts(List<StockProduct> products);
        SystemIndex GetIndexs();
        void UpdateIndexs(SystemIndex indexs);
        bool Save();
    }
}
