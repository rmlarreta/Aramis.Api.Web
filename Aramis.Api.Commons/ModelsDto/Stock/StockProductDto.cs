namespace Aramis.Api.Commons.ModelsDto.Stock
{
    public class StockProductDto
    {
        public Guid Id { get; set; }

        public decimal Cantidad { get; set; }

        public string Plu { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public Guid Rubro { get; set; }
        public string? RubroName { get; set; }

        public Guid Iva { get; set; }
        public decimal? IvaValue { get; set; }

        public decimal Costo { get; set; }

        public decimal Internos { get; set; }

        public decimal Tasa { get; set; }

        public bool Servicio { get; set; }
        public decimal? Unitario { get { return (Costo * (1 + (IvaValue / 100)) * (1 + (Tasa / 100))) + Internos; } } 
    }
}
