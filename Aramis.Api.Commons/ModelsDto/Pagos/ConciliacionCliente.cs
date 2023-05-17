using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.Commons.ModelsDto.Suppliers;

namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class ConciliacionCliente
    {
        public List<BusOperacionesDto>? OperacionesImpagas { get; set; }
        public List<CobReciboDetalleDto>? DetallesCuentaCorriente { get; set; }
        public List<CobReciboDto>? RecibosSinImputar { get; set; }
        public List<OpDocumentoProveedorDto>? FacturasImpagas { get; set; }
        public decimal? Debitos => DetallesCuentaCorriente!.Sum(x => x.Monto);
        public decimal? Creditos => RecibosSinImputar!.Sum(x => x.Detalles!.Sum(x => x.Monto)) + FacturasImpagas!.Sum(x => x.Monto);
        public decimal? SaldoConciliado => Debitos - Creditos;

    }
}
