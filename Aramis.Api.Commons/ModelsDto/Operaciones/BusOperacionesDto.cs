using Aramis.Api.Commons.Helpers;

namespace Aramis.Api.Commons.ModelsDto.Operaciones
{
    public class BusOperacionesDto
    {
        public Guid Id { get; set; }

        public int? Numero { get; set; }    

        public Guid ClienteId { get; set; }

        public string? Cui { get; set; }

        public string? Resp { get; set; }

        public string? Domicilio { get; set; }

        public DateTime Fecha { get; set; }

        public DateTime Vence { get; set; }

        public string Razon { get; set; } = null!;

        public string? CodAut { get; set; }

        public Guid TipoDocId { get; set; }

        public string? TipoDocName { get; set; }

        public Guid EstadoId { get; set; }

        public string? EstadoName { get; set; }

        public int Pos { get; set; }

        public string Operador { get; set; } = null!;

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
        public string CuitEmpresa { get; set; } = null!;

        public string RazonEmpresa { get; set; } = null!;

        public string DomicilioEmpresa { get; set; } = null!;

        public string Fantasia { get; set; } = null!;

        public string Iibb { get; set; } = null!;

        public DateTime Inicio { get; set; }

        public string RespoEmpresa { get; set; } = null!;

    }
}
