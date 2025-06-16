using System;

namespace EmailOrderApp.Domain.Entities;

public class EmailMessage
{
    public int Id { get; set; }
    public string BodyText { get; set; } = string.Empty;
    public string BodyHtml { get; set; } = string.Empty;
    public byte[]? EmlFile { get; set; }
    public string MessageId { get; set; } = string.Empty;
}
