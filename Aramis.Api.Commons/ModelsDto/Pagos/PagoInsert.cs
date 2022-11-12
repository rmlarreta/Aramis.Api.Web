namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class PagoInsert
    {
        public Guid ReciboId { get; set; }

        #region Documentos
        public List<string> Operaciones { get; set; } = new List<string>();
        #endregion

    }
}
