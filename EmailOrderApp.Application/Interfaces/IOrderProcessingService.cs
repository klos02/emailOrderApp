using System;
using EmailOrderApp.Application.DTOs;

namespace EmailOrderApp.Application.Interfaces;

public interface IOrderProcessingService
{
    Task<int> ParseAndSaveOrdersAsync();
    Task<List<OrderDto>> GetAllAsync();


}
