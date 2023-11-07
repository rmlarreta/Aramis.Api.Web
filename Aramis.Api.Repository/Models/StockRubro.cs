using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class StockRubro : Entity
{

    public string Name { get; set; } = null!;

    public virtual ICollection<StockProduct> StockProducts { get; } = new List<StockProduct>();
}
