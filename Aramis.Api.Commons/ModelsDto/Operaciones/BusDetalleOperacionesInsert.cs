namespace Aramis.Api.Commons.ModelsDto.Operaciones
{
    public class BusDetalleOperacionesInsert
    {
        public Guid? Id { get; set; } = null;

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

        public decimal? TotalNeto10 => IvaValue.Equals(10.5m) ? Math.Round((Unitario - Internos) / 1.105m * Cantidad, 2) : 0.0m;
        public decimal? TotalNeto21 => IvaValue.Equals(21.0m) ? Math.Round((Unitario - Internos) / 1.21m * Cantidad, 2) : 0.0m;
        public decimal? TotalExento => IvaValue.Equals(0.0m) ? Math.Round((Unitario - Internos) * Cantidad, 2) : 0.0m;
        public decimal? TotalIva => Math.Round((decimal)((TotalNeto10 + TotalNeto21) * IvaValue / 100)!, 2);
        public decimal? Total => Math.Round((decimal)(TotalInternos + (TotalNeto10 + TotalNeto21) + TotalIva + TotalExento)!, 2);
        public decimal? TotalIva10 => IvaValue.Equals(10.5m) ? TotalIva : 0.0m;
        public decimal? TotalIva21 => IvaValue.Equals(21.0m) ? TotalIva : 0.0m;
        public decimal? TotalNeto => Math.Round((decimal)(TotalNeto10 + TotalNeto21)!, 2);
        public string? Operador { get; set; }
    }
}
