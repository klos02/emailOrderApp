using System;
using EmailOrderApp.Application.DTOs;

namespace EmailOrderApp.Application.Interfaces;

public interface IEmailProcessingService
{
    Task FetchAndProcessEmailsAsync();
    Task<List<EmailMessageDto>> GetEmailsAsync();
}
