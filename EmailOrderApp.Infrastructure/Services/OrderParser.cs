using System;
using System.Text.Json;
using EmailOrderApp.Application.Interfaces;
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

        string prompt = """
        Jesteś parserem zamówień.  
        Otrzymasz treść maila z zamówieniem w języku polskim.  

        Wyodrębnij z niego następujące dane i zwróć JEDYNIE w formacie JSON:

        {
        "OrderNumber": int,
        "OrderDate": "YYYY-MM-DD",
        "CustomerName": string,
        "TotalAmount": decimal,
        "OrderItems": [
            {
            "Name": string,
            "Count": int,
            "Price": decimal
            },
            ...
        ]
        }

        Zignoruj inne dane (adres, telefon, metoda płatności). Liczy się tylko poprawny JSON z powyższą strukturą.

        Oto treść maila:

        """;

        var result = new List<Order>();

        foreach (var mail in newMails)
        {
            var body = prompt + mail.BodyText;
            var response = await geminiClient.SendMessageAsync(body);
            try
            {
                var order = JsonToOrder(response);


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

    private static Order JsonToOrder(string response)
    {
        var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(response);

        if (geminiResponse?.candidates?.FirstOrDefault()?.content?.parts?.FirstOrDefault()?.text is not string rawText)
            throw new Exception("Brak tekstu w odpowiedzi modelu");


        rawText = rawText.Replace("```json", "")
                         .Replace("```", "")
                         .Trim();


        var order = JsonSerializer.Deserialize<Order>(rawText, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return order ?? throw new Exception("Nie udało się zdeserializować zamówienia");
    }
}

