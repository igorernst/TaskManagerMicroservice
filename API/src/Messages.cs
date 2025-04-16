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
