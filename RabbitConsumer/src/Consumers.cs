using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

public class TaskUpdatedConsumer : IConsumer<TaskUpdated>
{
    public Task Consume(ConsumeContext<TaskUpdated> context)
    {
        var msg = context.Message;
        Console.WriteLine($"Task updated [{msg.UpdatedAt:u}], Id: {msg.Id}");
        return Task.CompletedTask;
    }
}

public class TaskCreatedConsumer : IConsumer<TaskCreated>
{
    public Task Consume(ConsumeContext<TaskCreated> context)
    {
        var msg = context.Message;
        Console.WriteLine($"Task created: {msg.Title}, Id:{msg.Id}");
        return Task.CompletedTask;
    }
}

public class TaskDeletedConsumer : IConsumer<TaskDeleted>
{
    public Task Consume(ConsumeContext<TaskDeleted> context)
    {
        var msg = context.Message;
        Console.WriteLine($"Task deleted: Id: {msg.Id}");
        return Task.CompletedTask;
    }
}
