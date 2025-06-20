using System.Globalization;
using System.Text;
using DotNetEnv;
using EmailOrderApp.Application.Interfaces;
using EmailOrderApp.Application.Services;
using EmailOrderApp.BlazorServerUI.Components;
using EmailOrderApp.Domain.Interfaces;
using EmailOrderApp.Infrastructure.AIClients;
using EmailOrderApp.Infrastructure.Data;
using EmailOrderApp.Infrastructure.Imap;
using EmailOrderApp.Infrastructure.Services;
using EmailOrderApp.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Radzen;

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

var geminiOptions = new GeminiSettings
{
    ApiKey = Environment.GetEnvironmentVariable("GeminiSettings_ApiKey"),
    Model = Environment.GetEnvironmentVariable("GeminiSettings_Model")
};

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 42)))
);

var cultureInfo = new CultureInfo("pl-PL");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;



builder.Services.AddScoped<IEmailReceiver, EmailReceiver>();
builder.Services.AddScoped<IEmailMessageRepository, EmailMessageRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderParser, OrderParser>();
builder.Services.AddScoped<INotParsedMails, NotParsedMails>();
builder.Services.AddScoped<IOrderProcessingService, OrderProcessingService>();
builder.Services.AddScoped<IEmailProcessingService, EmailProcessingService>();
builder.Services.AddScoped<IOrderSaver, OrderSaver>();
builder.Services.Configure<EmailSettings>(options =>
{
    options.ImapHost = emailOptions.ImapHost;
    options.ImapPort = emailOptions.ImapPort;
    options.Username = emailOptions.Username;
    options.Password = emailOptions.Password;
}
);
builder.Services.Configure<GeminiSettings>(options =>
{
    options.ApiKey = geminiOptions.ApiKey;
    options.Model = geminiOptions.Model;
}
);
builder.Services.AddHttpClient<GeminiClient>();

//builder.WebHost.UseUrls("http://*:80");
builder.Logging.AddConsole();

builder.Services.AddScoped<NotificationService>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}


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
