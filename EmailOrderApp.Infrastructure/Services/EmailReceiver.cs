using System;
using System.Text;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Domain.Entities;
using EmailOrderApp.Domain.Interfaces;
using EmailOrderApp.Infrastructure.Imap;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailOrderApp.Infrastructure.Services;

public class EmailReceiver(IEmailMessageRepository repository, IUnitOfWork unitOfWork, IOptions<EmailSettings> options, ILogger<EmailReceiver> logger) : IEmailReceiver
{


    public async Task FetchAndSaveEmailAsync()
    {
        try
        {

            //logger.LogInformation("Start fetching emails...");
            using var client = new ImapClient();

            var settings = options.Value;

            await client.ConnectAsync(settings.ImapHost, settings.ImapPort, true);
            Console.WriteLine("Connected.");
            await client.AuthenticateAsync(settings.Username, settings.Password);
            Console.WriteLine("Authenticated.");

            var inbox = client.Inbox;

            await inbox.OpenAsync(FolderAccess.ReadOnly);

            foreach (var uid in await inbox.SearchAsync(SearchQuery.NotSeen))
            {
                var message = await inbox.GetMessageAsync(uid);

                if (await repository.ExistsByMessageIdAsync(message.MessageId)) continue;

                var email = new EmailMessage
                {
                    BodyHtml = message.HtmlBody ?? string.Empty,
                    BodyText = GetDecodedTextBody(message),
                    MessageId = message.MessageId ?? Guid.NewGuid().ToString(),
                    EmlFile = SaveAsEmlBytes(message)

                };

                await repository.AddAsync(email);
            }

            await unitOfWork.SaveChangesAsync();
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);

        }

    }

    private static byte[] SaveAsEmlBytes(MimeMessage message)
    {
        using var stream = new MemoryStream();
        message.WriteTo(stream);
        return stream.ToArray();
    }

    private static string GetDecodedTextBody(MimeMessage message)
    {

        var textPart = message.BodyParts.OfType<TextPart>().FirstOrDefault(tp => tp.IsPlain);

        if (textPart != null)
        {
            var charset = textPart.ContentType?.Charset ?? "utf-8";
            var encoding = Encoding.GetEncoding(charset);

            using var memory = new MemoryStream();
            textPart.Content.DecodeTo(memory);
            var bodyBytes = memory.ToArray();

            return encoding.GetString(bodyBytes);
        }

        return string.Empty;
    }


}
