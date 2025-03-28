using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int? Quantity { get; set; }

    public decimal Price { get; set; }

    public string Origin { get; set; } = null!;

    public int CategoryId { get; set; }

    public string Image { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
