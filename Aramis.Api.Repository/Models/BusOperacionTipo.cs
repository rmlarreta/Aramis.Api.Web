namespace Aramis.Api.Repository.Models;

public partial class BusOperacionTipo
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public string? CodeExt { get; set; }

    public int? TipoAfip { get; set; }

    public virtual ICollection<BusOperacion> BusOperacions { get; } = new List<BusOperacion>();
}
