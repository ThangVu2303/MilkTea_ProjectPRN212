using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int StaffId { get; set; }

    public int CustomerId { get; set; }

    public int? Point { get; set; }

    public decimal Total { get; set; }

    public DateOnly DateCreate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();

    public virtual Staff Staff { get; set; } = null!;
}
