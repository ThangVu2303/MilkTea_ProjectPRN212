using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class OrdersDetail
{
    public int OrderId { get; set; }

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
