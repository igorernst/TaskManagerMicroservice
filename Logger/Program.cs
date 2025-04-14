using Microsoft.Extensions.Logging;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/logs/tasks/new", (TaskNew entry, ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("AppLogger");
    var timestamp = entry.Timestamp ?? DateTime.UtcNow;
    var fullEntry = entry with { Timestamp = timestamp, logId = Guid.NewGuid()};
    var logText = JsonSerializer.Serialize(fullEntry);
    logger.LogInformation(logText);
    return Results.Ok(new { status = "ok", logId = fullEntry.logId });
})
.WithName("PostLogTaskNew")
.WithOpenApi();

app.MapPost("/api/logs/tasks/edit", (TaskEdit entry, ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("AppLogger");
    var timestamp = entry.Timestamp ?? DateTime.UtcNow;
    var fullEntry = entry with { Timestamp = timestamp, logId = Guid.NewGuid()};
    var logText = JsonSerializer.Serialize(fullEntry);
    logger.LogInformation(logText);
    return Results.Ok(new { status = "ok", logId = fullEntry.logId });
})
.WithName("PostLogTaskEdit")
.WithOpenApi();

app.MapPost("/api/logs/tasks/delete", (TaskDelete entry, ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("AppLogger");
    var timestamp = entry.Timestamp ?? DateTime.UtcNow;
    var fullEntry = entry with { Timestamp = timestamp, logId = Guid.NewGuid()};
    var logText = JsonSerializer.Serialize(fullEntry);
    logger.LogInformation(logText);
    return Results.Ok(new { status = "ok", logId = fullEntry.logId });
})
.WithName("PostLogTaskDelete")
.WithOpenApi();

app.Run();
