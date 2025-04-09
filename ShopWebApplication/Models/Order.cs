using System;
using System.Collections.Generic;

namespace ShopWebApplication.Models;

public partial class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime OrderDate { get; set; }
}
