using System;
using EmailOrderApp.Domain.Entities;
using EmailOrderApp.Domain.Interfaces;
using EmailOrderApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace EmailOrderApp.Infrastructure.Services;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task AddAsync(Order order)
    {
        await context.Orders.AddAsync(order);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();

    }

    public async Task<Order?> GetByOrderNumberAsync(int orderNumber)
    {
        return await context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
    }
}
