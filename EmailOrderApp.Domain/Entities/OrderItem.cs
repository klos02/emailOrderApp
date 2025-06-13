using System;

namespace EmailOrderApp.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Count { get; set; }
    public int Price { get; set; }
}
