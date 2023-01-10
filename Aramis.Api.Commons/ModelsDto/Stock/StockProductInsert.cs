namespace Aramis.Api.Commons.ModelsDto.Stock
{
    public class StockProductInsert
    {
        public Guid? Id { get; set; } =null;

        public decimal Cantidad { get; set; }

        public string Plu { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public Guid Rubro { get; set; }
        public Guid Iva { get; set; }

        public decimal Neto { get; set; }

        public decimal Internos { get; set; }

        public decimal Tasa { get; set; }
        public bool Servicio { get; set; }
    }
}
