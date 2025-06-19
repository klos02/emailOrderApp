using System;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Domain.Interfaces;

namespace EmailOrderApp.Infrastructure.Services;

public class OrderSaver(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderParser orderParser) : IOrderSaver
{
    public async Task<int> SaveNewOrdersAsync()
    {
        int count = 0;
        var orders = await orderParser.ParseOrderFromEmailAsync();

        foreach (var order in orders)
        {
            await orderRepository.AddAsync(order);
            count++;
        }

        await unitOfWork.SaveChangesAsync(); // nie zwracam tego, bo zamiast liczby samych zamówień zwraca także liczbę przedmiotów w zamówieniach
        return count;
    }
}
