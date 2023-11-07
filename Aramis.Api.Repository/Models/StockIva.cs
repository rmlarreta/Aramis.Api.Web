using Aramis.Api.Repository.Application.Commons;

namespace Aramis.Api.Repository.Models;

public partial class StockIva : Entity
{
    public decimal Value { get; set; }

    public virtual ICollection<StockProduct> StockProducts { get; } = new List<StockProduct>();
}
