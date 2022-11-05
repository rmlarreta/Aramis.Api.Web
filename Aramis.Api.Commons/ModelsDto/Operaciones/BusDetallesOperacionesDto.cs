namespace Aramis.Api.Commons.ModelsDto.Operaciones
{
    public class BusDetallesOperacionesDto
    {
        public Guid Id { get; set; }

        public Guid OperacionId { get; set; }

        public decimal Cantidad { get; set; }

        public Guid ProductoId { get; set; }

        public string Codigo { get; set; } = null!;

        public string Detalle { get; set; } = null!;

        public string Rubro { get; set; } = null!;
        public decimal Unitario { get; set; }

        public decimal IvaValue { get; set; }
        public decimal Internos { get; set; }

        public decimal Facturado { get; set; }
        public decimal? TotalInternos => Internos * Cantidad;
        public decimal? TotalNeto => IvaValue.Equals(0.0m) ? 0.0m : Unitario * Cantidad;
        public decimal? TotalIva => TotalNeto * IvaValue / 100;
        public decimal? Total => TotalInternos + TotalNeto + TotalIva + TotalExento;
        public decimal? TotalIva10 => IvaValue.Equals(10.5m) ? TotalIva : 0.0m;
        public decimal? TotalIva21 => IvaValue.Equals(21.0m) ? TotalIva : 0.0m;
        public decimal? TotalExento => IvaValue.Equals(0.0m) ? Unitario * Cantidad : 0.0m;

    }
}
