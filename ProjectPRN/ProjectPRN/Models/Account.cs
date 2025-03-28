using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public int RoleId { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual Role Role { get; set; } = null!;

    public virtual Staff? Staff { get; set; }
}
