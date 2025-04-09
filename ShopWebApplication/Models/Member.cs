using System;
using System.Collections.Generic;

namespace ShopWebApplication.Models;

public partial class Member
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public string Email { get; set; } = null!;
}
