
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PostNewTaskHandler>();
builder.Services.AddScoped<PostEditTaskHandler>();
builder.Services.AddScoped<PostDeleteTaskHandler>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/logs/tasks/new", (TaskNew entry, PostNewTaskHandler handler) => handler.Handle(entry))
.WithName("PostLogTaskNew")
.WithOpenApi();

app.MapPost("/api/logs/tasks/edit", (TaskEdit entry, PostEditTaskHandler handler) => handler.Handle(entry))
.WithName("PostLogTaskEdit")
.WithOpenApi();

app.MapPost("/api/logs/tasks/delete", (TaskDelete entry, PostDeleteTaskHandler handler) => handler.Handle(entry))
.WithName("PostLogTaskDelete")
.WithOpenApi();

app.Run();
