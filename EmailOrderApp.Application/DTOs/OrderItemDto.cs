using System;

namespace EmailOrderApp.Application.DTOs;

public class OrderItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Count { get; set; }
    public decimal Price { get; set; }
}
