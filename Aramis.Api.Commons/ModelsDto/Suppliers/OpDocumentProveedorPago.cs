namespace Aramis.Api.Commons.ModelsDto.Suppliers
{
    public class OpDocumentProveedorPago
    {
        public OpDocumentoProveedorDto? Documento { get; set; }
        public Guid Cuenta { get; set; }
        public string? Operador { get; set; }

    }
}
