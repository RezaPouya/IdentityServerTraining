using OrderProcessing.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

await app.ConfigureMiddlewares();

app.Run();