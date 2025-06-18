using System;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Domain.Interfaces;

namespace EmailOrderApp.Infrastructure.Services;

public class OrderSaver(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderParser orderParser) : IOrderSaver
{
    public async Task<int> SaveNewOrdersAsync()
    {
        var orders = await orderParser.ParseOrderFromEmailAsync();

        foreach (var order in orders)
        {
            await orderRepository.AddAsync(order);
        }

        return await unitOfWork.SaveChangesAsync();
    }
}
