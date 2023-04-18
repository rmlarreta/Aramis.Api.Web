using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Proveedores
{
    public interface ISuppliersRepository
    {
        void Add(OpDocumentoProveedor documento);
        List<OpDocumentoProveedor> GetAll();
        bool Save();
    }
}
