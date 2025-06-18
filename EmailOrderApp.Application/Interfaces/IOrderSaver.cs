using System;

namespace EmailOrderApp.Application.Interfaces;

public interface IOrderSaver
{
    Task<int> SaveNewOrdersAsync();
}
