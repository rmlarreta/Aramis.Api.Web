using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.FlowService.Application
{
    public class CuentasService : ICuentasService
    {
        private readonly IGenericRepository<CobCuentum> _cuentas;
        public CuentasService(IGenericRepository<CobCuentum> cuentas)
        {
            _cuentas = cuentas;
        }
        public bool Delete(string id)
        {
             _cuentas.Delete(Guid.Parse(id));
            return _cuentas.Save();
        }

        public List<CobCuentum> GetAll()
        {
            return _cuentas.Get().ToList();
        }

        public CobCuentum GetById(string id)
        {
            return _cuentas.Get(Guid.Parse(id));
        }

        public CobCuentum Insert(CobCuentum cobCuentum)
        {
            cobCuentum.Id = Guid.NewGuid();
            _cuentas.Add(cobCuentum);
            return cobCuentum;
        }

        public CobCuentum Update(CobCuentum cobCuentum)
        {
           _cuentas.Update(cobCuentum);
            return cobCuentum;
        }
    }
}
