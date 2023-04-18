using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Proveedores;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application.Proveedores
{
    public class SuppliersRepository : ISuppliersRepository
    {
        private readonly IGenericRepository<OpDocumentoProveedor> _opDocumento;

        public SuppliersRepository(IGenericRepository<OpDocumentoProveedor> opDocumento)
        {
            _opDocumento = opDocumento;
        }

        public void Add(OpDocumentoProveedor documento)
        {
            _opDocumento.Add(documento);
        }

        public List<OpDocumentoProveedor> GetAll()
        {
            return _opDocumento.Get().ToList();
        }

        public bool Save() => _opDocumento.Save();
    }
}
