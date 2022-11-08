using Aramis.Api.Repository.Models;
namespace Aramis.Api.Repository.Interfaces.Recibos
{
    public interface IRecibosRepository
    {
        void Add(CobRecibo recibo);
        CobRecibo Get(string id);
        IEnumerable<CobRecibo> GetAll();
        void Update(CobRecibo recibo);
        void Delete(string id);
        bool Save();
    }
}
