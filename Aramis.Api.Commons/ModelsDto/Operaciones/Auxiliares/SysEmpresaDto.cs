namespace Aramis.Api.Commons.ModelsDto.Operaciones
{
    public class SysEmpresaDto
    {
        public string CuitEmpresa { get; set; } = null!;

        public string RazonEmpresa { get; set; } = null!;

        public string DomicilioEmpresa { get; set; } = null!;

        public string Fantasia { get; set; } = null!;

        public string Iibb { get; set; } = null!;

        public DateTime Inicio { get; set; }

        public string RespoEmpresa { get; set; } = null!;
    }
}
