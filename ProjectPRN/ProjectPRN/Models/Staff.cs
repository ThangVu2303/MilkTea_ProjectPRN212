using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public DateOnly HireDate { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
