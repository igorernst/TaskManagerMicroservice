namespace Messages;

public interface TaskCreated 
{
    int Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    int Status { get; set; }
}

public interface TaskUpdated 
{
    int Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    int Status { get; set; }
}

public interface TaskDeleted 
{
    int Id {get; set;}
}

// public record CreateTaskEvent(string Title, string Description, DateTime CreatedAt, DateTime UpdatedAt);

// public record ChangeTaskStatusEvent(int Id, int StatusId);

// public record DeleteTaskEvent(int Id);
