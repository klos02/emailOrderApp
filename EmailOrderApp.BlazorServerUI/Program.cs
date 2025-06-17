using System.Text;
using DotNetEnv;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.BlazorServerUI.Components;
using EmailOrderApp.Domain.Interfaces;
using EmailOrderApp.Infrastructure.Data;
using EmailOrderApp.Infrastructure.Imap;
using EmailOrderApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
string connectionString = $"server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                          $"port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                          $"database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                          $"uid={Environment.GetEnvironmentVariable("DB_USER")};" +
                          $"password={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

var emailOptions = new EmailSettings
{
    ImapHost = Environment.GetEnvironmentVariable("EmailSettings_ImapHost"),
    ImapPort = int.TryParse(Environment.GetEnvironmentVariable("EmailSettings_ImapPort"), out var port) ? port : 993,
    Username = Environment.GetEnvironmentVariable("EmailSettings_Username"),
    Password = Environment.GetEnvironmentVariable("EmailSettings_Password"),
};

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddScoped<IEmailReceiver, EmailReceiver>();
builder.Services.AddScoped<IEmailMessageRepository, EmailMessageRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<EmailSettings>(options =>
{
    options.ImapHost = emailOptions.ImapHost;
    options.ImapPort = emailOptions.ImapPort;
    options.Username = emailOptions.Username;
    options.Password = emailOptions.Password;
}
);


builder.Logging.AddConsole();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
