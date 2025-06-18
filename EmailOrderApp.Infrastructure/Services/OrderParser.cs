using System;
using System.Text.Json;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Application.Prompts;
using EmailOrderApp.Domain.Entities;
using EmailOrderApp.Infrastructure.AIClients;
using EmailOrderApp.Infrastructure.Settings;
using Microsoft.Extensions.Options;


namespace EmailOrderApp.Infrastructure.Services;

public class OrderParser(INotParsedMails notParsedMails, IOptions<GeminiSettings> options, GeminiClient geminiClient) : IOrderParser
{
    public async Task<IEnumerable<Order>> ParseOrderFromEmailAsync()
    {

        var newMails = await notParsedMails.GetNotParsedMailsAsync();

        var prompt = GeminiOrderPrompt.GetPrompt();

        var result = new List<Order>();

        foreach (var mail in newMails)
        {
            var body = prompt + mail.BodyText;
            var response = await geminiClient.SendMessageAsync(body);
            try
            {
                var order = geminiClient.JsonToOrder(response);


                if (order != null)
                {

                    order.MessageId = mail.MessageId;

                    result.Add(order);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Nie udało się sparsować zamówienia z maila {mail.MessageId}: {ex.Message}");
            }


        }
        return result;
    }

    
}

