using System;
using System.Collections.Generic;

namespace Aramis.Api.Repository.Models;

public partial class StockIva
{
    public Guid Id { get; set; }

    public decimal Value { get; set; }

    public virtual ICollection<StockProduct> StockProducts { get; } = new List<StockProduct>();
}
