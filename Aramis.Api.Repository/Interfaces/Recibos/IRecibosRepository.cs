using Aramis.Api.Repository.Models;
namespace Aramis.Api.Repository.Interfaces.Recibos
{
    public interface IRecibosRepository
    {
        void Add(CobRecibo recibo);
        CobRecibo Get(string id);
        List<CobRecibo> GetAll();
        List<CobRecibo> GetSinImputarByCLiente(string clienteId);
        List<CobReciboDetalle> GetCuentaCorrientesByCliente(string clienteId);
        void Update(CobRecibo recibo);
        void Delete(string id);
        SystemIndex GetIndexs();
        void UpdateIndexs(SystemIndex indexs);
        bool Save();
    }
}
