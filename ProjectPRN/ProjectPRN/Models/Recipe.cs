using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string ProductId { get; set; } = null!;

    public int MaterialId { get; set; }

    public double QuantityRequired { get; set; }

    public virtual RawMaterial Material { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
