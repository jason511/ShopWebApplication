using System;
using System.Collections.Generic;

namespace ShopWebApplication.Models;

public partial class Shoppingcar
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }
}
