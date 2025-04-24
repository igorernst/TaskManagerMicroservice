using MassTransit;

namespace api;

public class GetTaskByIdHandler {

    private readonly IRepository<TaskEntity> _taskRepository;

    public GetTaskByIdHandler(IRepository<TaskEntity> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public TaskEntity Handle(int id)
    {
        return _taskRepository.GetById(id);
    }
}

public class GetTasksHandler {

    private readonly IRepository<TaskEntity> _taskRepository;

    public GetTasksHandler(IRepository<TaskEntity> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public IEnumerable<TaskEntity> Handle()
    {
        return _taskRepository.GetAll();
    }
}


public class PostTaskHandler {

    private readonly IRepository<TaskEntity> _taskRepository;
    private readonly ILoggingService _loggingService;
    private readonly IPublishEndpoint _publishEndpoint;

    public PostTaskHandler(IRepository<TaskEntity> taskRepository, ILoggingService loggingService, IPublishEndpoint publishEndpoint)
    {
        _taskRepository = taskRepository;
        _loggingService = loggingService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<int> Handle(TaskEntity task)
    {
        var id = _taskRepository.Add(task);
        var loggingTask = _loggingService.PostLogTaskNewAsync(new TaskNew(id, task.Title, task.Description, task.CreatedAt, task.Status));
        var publishTask = _publishEndpoint.Publish<Messages.TaskCreated>(new
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
    }
}

public class PutTaskHandler {

    private readonly IRepository<TaskEntity> _taskRepository;
    private readonly ILoggingService _loggingService;
    private readonly IPublishEndpoint _publishEndpoint;

    public PutTaskHandler(IRepository<TaskEntity> taskRepository, ILoggingService loggingService, IPublishEndpoint publishEndpoint)
    {
        _taskRepository = taskRepository;
        _loggingService = loggingService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(TaskEntity task, int id)
    {
        task.Id = id;
        _taskRepository.Update(task);
        var loggingTask = _loggingService.PostLogTaskEditAsync(new TaskEdit(id, task.Title, task.Description, task.CreatedAt, task.Status));
        var publishTask = _publishEndpoint.Publish<Messages.TaskUpdated>(new
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt,
            Status = task.Status
        });
        await Task.WhenAll(new List<Task>() { loggingTask, publishTask });
    }
}

public class DeleteTaskHandler {

    private readonly IRepository<TaskEntity> _taskRepository;
    private readonly ILoggingService _loggingService;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteTaskHandler(IRepository<TaskEntity> taskRepository, ILoggingService loggingService, IPublishEndpoint publishEndpoint)
    {
        _taskRepository = taskRepository;
        _loggingService = loggingService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(int id)
    {
        var task = _taskRepository.GetById(id);
        if (task is null)
        {
            return;
        }
        _taskRepository.Delete(id);
        var loggingTask = _loggingService.PostLogTaskDeleteAsync(new TaskDelete(id));
        var publishTask = _publishEndpoint.Publish<Messages.TaskDeleted>(new
        {
            Id = id
        });
        await Task.WhenAll(new List<Task>() { loggingTask, publishTask });
    }
}
