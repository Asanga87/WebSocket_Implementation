using WSAPP.Logics;
using WSAPP.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IMessageBuilderService, MessageHandlercs>();

builder.WebHost.UseUrls("http://localhost:6767");

var app = builder.Build();

// <snippet_UseWebSockets>
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};

app.UseWebSockets(webSocketOptions);
// </snippet_UseWebSockets>

app.UseDefaultFiles();

app.UseStaticFiles();

app.MapControllers();

app.Run();