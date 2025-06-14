using System;
using EmailOrderApp.Domain.Interfaces;
using EmailOrderApp.Infrastructure.Data;

namespace EmailOrderApp.Infrastructure.Services;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}
