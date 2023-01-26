using Aramis.Api.Commons.ModelsDto.Operaciones;
namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class ConciliacionCliente
    {
        public List<BusOperacionesDto>? OperacionesImpagas { get; set; }
        public List<CobReciboDetalleDto>? DetallesCuentaCorriente { get; set; }
        public List<CobReciboDto>? RecibosSinImputar { get; set; }
        public decimal? Debitos => DetallesCuentaCorriente!.Sum(x => x.Monto);
        public decimal? Creditos => RecibosSinImputar!.Sum(x => x.Detalles!.Sum(x => x.Monto));
        public decimal? SaldoConciliado => Debitos - Creditos;

    }
}
