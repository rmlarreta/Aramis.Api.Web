using Aramis.Api.Repository.Models;
namespace Aramis.Api.Repository.Interfaces.Recibos
{
    public interface IRecibosRepository
    {
        void Add(CobRecibo recibo);
        void Add(CobReciboDetalle detalle);
        CobRecibo Get(string id);
        CobReciboDetalle GetDetalle(string id);
        List<CobRecibo> GetAll();
        List<CobRecibo> GetSinImputarByCLiente(string clienteId);
        List<CobReciboDetalle> GetCuentaCorrientesByCliente(string clienteId);
        void Update(CobRecibo recibo);
        void Update(CobReciboDetalle detalle);
        void Delete(string id);
        SystemIndex GetIndexs();
        void UpdateIndexs(SystemIndex indexs);
        decimal GetTotal(string reciboId);
        bool Save();
    }
}
