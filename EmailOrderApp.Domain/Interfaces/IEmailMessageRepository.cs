using System;
using EmailOrderApp.Domain.Entities;

namespace EmailOrderApp.Domain.Interfaces;

public interface IEmailMessageRepository
{
    Task AddAsync(EmailMessage message);

    Task<bool> ExistsByMessageIdAsync(string messageId);

    Task<List<EmailMessage>> GetAllAsync();
}
