using System;

namespace EmailOrderApp.Application.Prompts;

public static class GeminiOrderPrompt
{
    public static string GetPrompt() => """
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
}
