using Aramis.Api.Commons.Helpers;

namespace Aramis.Api.Commons.ModelsDto.Operaciones
{
    public class BusOperacionesDto : BusOperacionBaseDto
    {

        public string? Cui { get; set; }

        public string? Resp { get; set; }

        public string? Domicilio { get; set; }

        public string? TipoDocName { get; set; }

        public string? EstadoName { get; set; }

        public decimal Total => Detalles!.Sum(x => x.Total) ?? 0.0m;

        public string? TotalLetras => ExtensionMethods.NumeroLetras(Total);

        public decimal? TotalInternos => Detalles!.Sum(x => x.TotalInternos);

        public decimal? TotalNeto => Detalles!.Sum(x => x.TotalNeto);

        public decimal? TotalIva => Detalles!.Sum(x => x.TotalIva);

        public decimal? TotalIva10 => Detalles!.Sum(x => x.TotalIva10);

        public decimal? TotalIva21 => Detalles!.Sum(x => x.TotalIva21);

        public decimal? TotalExento => Detalles!.Sum(x => x.TotalExento);

        //DETALLES 

        public List<BusDetallesOperacionesDto>? Detalles { get; set; } = new List<BusDetallesOperacionesDto>();
        public List<BusObservacionesDto> Observaciones { get; set; } = new List<BusObservacionesDto>();

        //Empresa 
        public SysEmpresaDto Empresa { get; set; } = new SysEmpresaDto();

    }
}
