using Microsoft.EntityFrameworkCore;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskAPIDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("TaskApiDatabase")));
builder.Services.Configure<LoggingApiSettings>(builder.Configuration.GetSection("LoggingServiceApi"));
builder.Services.AddScoped<LoggingService>();
builder.Services.AddScoped<TaskRepository>();

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

app.MapGet("/tasks", (TaskRepository repo) =>
{
    return repo.GetAll();
})
.WithName("GetTasks")
.WithOpenApi();

app.MapGet("/tasks/{id}", (TaskRepository tasks, int id) =>
{
    return tasks.GetById(id);
})
.WithName("GetTask")
.WithOpenApi();

app.MapPost("/tasks", async (TaskEntity task, TaskRepository tasks, LoggingService logging, IPublishEndpoint publishEndpoint) =>
{
    var id = tasks.Add(task);
    var loggingTask = logging.PostLogTaskNewAsync(new TaskNew(id, task.Title, task.Description, task.CreatedAt, task.Status));
    var publishTask = publishEndpoint.Publish<Messages.TaskCreated>(new
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        CreatedAt = task.CreatedAt,
        UpdatedAt = task.UpdatedAt,
        Status = task.Status
    });
    await Task.WhenAll(new List<Task>() { loggingTask, publishTask });
    return id;
})
.WithName("CreateTask")
.WithOpenApi();

app.MapPut("/tasks/{id}", async (TaskEntity task, int id, TaskRepository tasks, LoggingService logging, IPublishEndpoint publishEndpoint) =>
{
    task.Id = id;
    tasks.Update(task);
    var loggingTask = logging.PostLogTaskEditAsync(new TaskEdit(id, task.Title, task.Description, task.CreatedAt, task.Status));
    var publishTask = publishEndpoint.Publish<Messages.TaskUpdated>(new
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        CreatedAt = task.CreatedAt,
        UpdatedAt = task.UpdatedAt,
        Status = task.Status
    });
    await Task.WhenAll(new List<Task>() { loggingTask, publishTask });
})
.WithName("EditTask")
.WithOpenApi();

app.MapDelete("/tasks/{id}", async (int id, TaskRepository tasks, LoggingService logging, IPublishEndpoint publishEndpoint) =>
{
    var task = tasks.GetById(id);
    if (task is null)
    {
        return;
    }
    tasks.Delete(id);
    var loggingTask = logging.PostLogTaskDeleteAsync(new TaskDelete(id));
    var publishTask = publishEndpoint.Publish<Messages.TaskDeleted>(new
    {
        Id = id
    });
    await Task.WhenAll(new List<Task>() { loggingTask, publishTask });
})
.WithName("DeleteTask")
.WithOpenApi();

app.Run();

