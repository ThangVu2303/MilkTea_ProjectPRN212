using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Email { get; set; } = null!;

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}
