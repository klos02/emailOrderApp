using System;

namespace EmailOrderApp.Application.Interfaces;

public interface IEmailReceiver
{
    Task FetchAndSaveEmailAsync();
}
