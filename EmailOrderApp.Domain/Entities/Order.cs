using System;

namespace EmailOrderApp.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
    public string MessageId { get; set; } = string.Empty;
     
}
