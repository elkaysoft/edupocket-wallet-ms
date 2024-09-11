using Edupocket.Api;

var builder = WebApplication.CreateBuilder(args);


var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Wallet API is starting");

app.Run();


