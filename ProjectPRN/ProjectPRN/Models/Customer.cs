using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? Point { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
