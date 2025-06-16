using System;
using EmailOrderApp.Domain.Entities;
using EmailOrderApp.Domain.Interfaces;

namespace EmailOrderApp.Infrastructure.Services;

public class EmailMessageRepository : IEmailMessageRepository
{
    public async Task AddAsync(EmailMessage message)
    {
        
    }
}
