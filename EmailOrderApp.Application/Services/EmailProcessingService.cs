using System;
using EmailOrderApp.Application.DTOs;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Domain.Interfaces;

namespace EmailOrderApp.Application.Services;

public class EmailProcessingService(IEmailReceiver emailReceiver, IEmailMessageRepository emailMessageRepository) : IEmailProcessingService
{
    public async Task FetchAndProcessEmailsAsync()
    {
        await emailReceiver.FetchAndSaveEmailAsync();

    }

    public async Task<List<EmailMessageDto>> GetEmailsAsync()
    {
        var emails = await emailMessageRepository.GetAllAsync();
        List<EmailMessageDto> emailDtos = [];

        foreach (var email in emails)
        {
            var emailDto = new EmailMessageDto
            {
                Id = email.Id,
                BodyText = email.BodyText
            };

            emailDtos.Add(emailDto);
        }

        return emailDtos;
    }
}
