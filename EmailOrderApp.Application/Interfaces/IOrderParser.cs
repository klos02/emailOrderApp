using System;
using EmailOrderApp.Domain.Entities;

namespace EmailOrderApp.Application.Interfaces;

public interface IOrderParser
{
    Task<IEnumerable<Order>> ParseOrderFromEmailAsync();
}
