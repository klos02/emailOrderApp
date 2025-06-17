using System;
using EmailOrderApp.Domain.Entities;
using EmailOrderApp.Domain.Interfaces;
using EmailOrderApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmailOrderApp.Infrastructure.Services;

public class EmailMessageRepository(AppDbContext context) : IEmailMessageRepository
{
    public async Task AddAsync(EmailMessage message)
    {
        context.EmailMessages.Add(message);
    }

    public async Task<bool> ExistsByMessageIdAsync(string messageId)
    {
        return await context.EmailMessages.AnyAsync(e => e.MessageId == messageId);
    }

    public async Task<List<EmailMessage>> GetAllAsync()
    {
        return await context.EmailMessages.ToListAsync();
    }
}
