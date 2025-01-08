var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Databae")!);
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();
app.Run();
