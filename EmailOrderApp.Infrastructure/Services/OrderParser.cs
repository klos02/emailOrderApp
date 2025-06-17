using System;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Domain.Entities;
using OpenAI;

namespace EmailOrderApp.Infrastructure.Services;

public class OrderParser(OpenAIClient client) : IOrderParser
{
    public Task<Order> ParseOrderFromEmailAsync(string emailBody)
    {
        throw new NotImplementedException();
    }
}
