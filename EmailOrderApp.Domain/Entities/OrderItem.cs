using System;

namespace EmailOrderApp.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Count { get; set; }
    public decimal Price { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
