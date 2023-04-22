using Aramis.Api.Commons.ModelsDto.Suppliers;

namespace Aramis.Api.SupplierService.Interfaces
{
    public interface ISuppliers
    {
        bool InsertDocument(OpDocumentoProveedorDto documento);
        bool PagarDocumento(OpDocumentProveedorPago documentProveedorPago);
        bool UpdateDocument(OpDocumentoProveedorDto documento);
        List<OpDocumentoProveedorDto> GetByProveedor(string id);
        List<OpDocumentoProveedorDto> GetByState(string state);
        OpDocumentoProveedorDto GetById(string id);
    }
}
