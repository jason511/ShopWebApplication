using System;
using System.Collections.Generic;

namespace ShopWebApplication.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? Discount { get; set; }

    public string? Image { get; set; }

    public DateTime? UpdateDate { get; set; }
}
