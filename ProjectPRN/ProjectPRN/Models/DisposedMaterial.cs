using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class DisposedMaterial
{
    public int DisposedId { get; set; }

    public int MaterialId { get; set; }

    public double Quantity { get; set; }

    public DateTime DateDisposed { get; set; }

    public virtual RawMaterial Material { get; set; } = null!;
}
