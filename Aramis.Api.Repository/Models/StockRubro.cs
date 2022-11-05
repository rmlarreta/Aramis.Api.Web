namespace Aramis.Api.Repository.Models;

public partial class StockRubro
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StockProduct> StockProducts { get; } = new List<StockProduct>();
}
