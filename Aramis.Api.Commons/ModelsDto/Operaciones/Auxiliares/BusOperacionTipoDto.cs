namespace Aramis.Api.Commons.ModelsDto.Operaciones
{
    public class BusOperacionTipoDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Code { get; set; }

        public string? CodeExt { get; set; }

        public int? TipoAfip { get; set; }
    }
}
