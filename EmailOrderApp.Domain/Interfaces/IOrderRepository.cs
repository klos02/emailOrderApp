using System;
using EmailOrderApp.Domain.Entities;

namespace EmailOrderApp.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByOrderNumberAsync(int orderNumber);
    Task<IEnumerable<Order>> GetAllAsync();
    Task AddAsync(Order order);
}
