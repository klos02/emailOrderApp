using System;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Domain.Entities;

namespace EmailOrderApp.Infrastructure.Services;

public class OrderParser() : IOrderParser
{
    public Task<Order> ParseOrderFromEmailAsync(string emailBody)
    {
        throw new NotImplementedException();
    }
}
