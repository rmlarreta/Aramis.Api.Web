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
            var data = _opDocumento.Get().ToList();
            return data;
        }

        public OpDocumentoProveedor GetById(Guid id)
        {
            return _opDocumento.Get().Where(x=>x.Id.Equals(id)).FirstOrDefault()!;
            
        }

        public bool Save() => _opDocumento.Save();

        public void Update(OpDocumentoProveedor documento)
        {
            _opDocumento.Update(documento);
        }
    }
}
