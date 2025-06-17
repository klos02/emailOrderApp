using System;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Domain.Entities;
using EmailOrderApp.Domain.Interfaces;

namespace EmailOrderApp.Infrastructure.Services;

public class NotParsedMails(IEmailMessageRepository messageRepository, IOrderRepository orderRepository) : INotParsedMails
{
    public async Task<IEnumerable<EmailMessage>> GetNotParsedMailsAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        var messages = await messageRepository.GetAllAsync();

        var orderMessageIds = new HashSet<string>(orders.Select(o => o.MessageId));

        return messages.Where(m => !orderMessageIds.Contains(m.MessageId));
        
    }
}
