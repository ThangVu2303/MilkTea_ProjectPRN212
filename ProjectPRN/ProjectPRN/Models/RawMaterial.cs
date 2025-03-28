using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class RawMaterial
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal CostPerUnit { get; set; }

    public double Quantity { get; set; }

    public virtual ICollection<DisposedMaterial> DisposedMaterials { get; set; } = new List<DisposedMaterial>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
