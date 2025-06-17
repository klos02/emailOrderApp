using System;

namespace EmailOrderApp.Infrastructure.Imap;

public class EmailSettings
{
    public string ImapHost { get; set; } = string.Empty;
    public int ImapPort { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
