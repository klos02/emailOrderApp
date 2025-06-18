using System;

namespace EmailOrderApp.Application.DTOs;

public class EmailMessageDto
{
    public int Id { get; set; }
    public string BodyText { get; set; } = string.Empty;
}
