using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Proveedores
{
    public interface ISuppliersRepository
    {
        void Add(OpDocumentoProveedor documento);
        void Update(OpDocumentoProveedor documento);
        OpDocumentoProveedor GetById(Guid id);
        List<OpDocumentoProveedor> GetAll();
        bool Save();
    }
}
