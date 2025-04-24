using Microsoft.EntityFrameworkCore;
using MassTransit;
using api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskAPIDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("TaskApiDatabase")));
builder.Services.Configure<LoggingApiSettings>(builder.Configuration.GetSection("LoggingServiceApi"));
builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.AddScoped<IRepository<TaskEntity>, TaskRepository>();
builder.Services.AddScoped<GetTaskByIdHandler>();
builder.Services.AddScoped<GetTasksHandler>();
builder.Services.AddScoped<PostTaskHandler>();
builder.Services.AddScoped<PutTaskHandler>();
builder.Services.AddScoped<DeleteTaskHandler>();

var rabbitMqHost = builder.Configuration["LogPublisherConfig:HostName"];

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, config) =>
    {
        config.Host(rabbitMqHost, "/");
    });
});

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/tasks", (GetTasksHandler handler) => handler.Handle())
.WithName("GetTasks")
.WithOpenApi();

app.MapGet("/tasks/{id}", (GetTaskByIdHandler handler, int id) => handler.Handle(id))
.WithName("GetTask")
.WithOpenApi();

app.MapPost("/tasks", async (PostTaskHandler handler, TaskEntity task) => await handler.Handle(task))
.WithName("CreateTask")
.WithOpenApi();

app.MapPut("/tasks/{id}", async (PutTaskHandler handler, TaskEntity task, int id) => await handler.Handle(task, id))
.WithName("EditTask")
.WithOpenApi();

app.MapDelete("/tasks/{id}", async (DeleteTaskHandler handler, int id) => await handler.Handle(id))
.WithName("DeleteTask")
.WithOpenApi();

app.Run();

