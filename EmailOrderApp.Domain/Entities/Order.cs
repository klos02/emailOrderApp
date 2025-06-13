using System;

namespace EmailOrderApp.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
     
}
