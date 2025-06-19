using System;

namespace EmailOrderApp.Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = [];
    public bool IsExpanded { get; set; } = false;

     
}
