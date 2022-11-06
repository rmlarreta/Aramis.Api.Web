using Aramis.Api.Commons.ModelsDto.Operaciones;

namespace Aramis.Api.Commons.ModelsDto.Pagos
{
    public class PagoInsert
    {
        public Guid ReciboId { get; set; }

        public Guid ClienteId { get; set; }

        public DateTime Fecha { get; set; }

        public string Operador { get; set; } = null!;

        public decimal? Total => Detalles.Sum(x=>x.Monto);


        #region Detalle De Pagos
        public List<DetallesInsert> Detalles { get; set; } = new List<DetallesInsert>();
        #endregion

        #region Documentos
        public List<BusOperacionesDto> Operaciones { get; set; } = new List<BusOperacionesDto>();   
        #endregion

    }
}
