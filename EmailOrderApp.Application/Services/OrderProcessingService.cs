using System;
using EmailOrderApp.Application.DTOs;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Domain.Interfaces;

namespace EmailOrderApp.Application.Services;

public class OrderProcessingService(IOrderSaver orderSaver, IOrderRepository orderRepository) : IOrderProcessingService
{
    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await orderRepository.GetAllAsync();

        List<OrderDto> orderDtos = [];

        foreach (var order in orders)
        {
            var orderDto = new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                OrderNumber = order.OrderNumber,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    Count = item.Count,
                    Name = item.Name,
                    Price = item.Price

                }).ToList()
            };

            orderDtos.Add(orderDto);
        }

        return orderDtos;
    }

    public async Task<int> ParseAndSaveOrdersAsync()
    {
        return await orderSaver.SaveNewOrdersAsync();
    }
}
