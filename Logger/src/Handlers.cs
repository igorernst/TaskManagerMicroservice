using System.Text.Json;
using Microsoft.Extensions.Logging;
public class PostNewTaskHandler {

    private readonly ILoggerFactory _loggerFactory;

    public PostNewTaskHandler(ILoggerFactory loggerFactory) {
        _loggerFactory = loggerFactory;
    }
    public IResult Handle(TaskNew entry)
    {
        var logger = _loggerFactory.CreateLogger("AppLogger");
        var timestamp = entry.Timestamp ?? DateTime.UtcNow;
        var fullEntry = entry with { Timestamp = timestamp, logId = Guid.NewGuid()};
        var logText = JsonSerializer.Serialize(fullEntry);
        logger.LogInformation(logText);
        return Results.Ok(new { status = "ok", logId = fullEntry.logId });
    }
}
public class PostEditTaskHandler {

    private readonly ILoggerFactory _loggerFactory;

    public PostEditTaskHandler(ILoggerFactory loggerFactory) {
        _loggerFactory = loggerFactory;
    }
    public IResult Handle(TaskEdit entry)
    {
        var logger = _loggerFactory.CreateLogger("AppLogger");
        var timestamp = entry.Timestamp ?? DateTime.UtcNow;
        var fullEntry = entry with { Timestamp = timestamp, logId = Guid.NewGuid()};
        var logText = JsonSerializer.Serialize(fullEntry);
        logger.LogInformation(logText);
        return Results.Ok(new { status = "ok", logId = fullEntry.logId });
    }
}
public class PostDeleteTaskHandler {

    private readonly ILoggerFactory _loggerFactory;

    public PostDeleteTaskHandler(ILoggerFactory loggerFactory) {
        _loggerFactory = loggerFactory;
    }
    public IResult Handle(TaskDelete entry)
    {
        var logger = _loggerFactory.CreateLogger("AppLogger");
        var timestamp = entry.Timestamp ?? DateTime.UtcNow;
        var fullEntry = entry with { Timestamp = timestamp, logId = Guid.NewGuid()};
        var logText = JsonSerializer.Serialize(fullEntry);
        logger.LogInformation(logText);
        return Results.Ok(new { status = "ok", logId = fullEntry.logId });
    }
}