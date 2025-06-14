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
        var orders = await context.Orders.ToListAsync();
        return orders;
    }

    public Task<Order?> GetByOrderNumberAsync(int OrderNumber)
    {
        throw new NotImplementedException();
    }
}
