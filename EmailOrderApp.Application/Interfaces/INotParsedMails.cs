using System;
using EmailOrderApp.Domain.Entities;

namespace EmailOrderApp.Application.Interfaces;

public interface INotParsedMails
{
    Task<IEnumerable<EmailMessage>> GetNotParsedMailsAsync();
}
