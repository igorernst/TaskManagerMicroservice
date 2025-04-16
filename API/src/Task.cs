
/// <summary>
/// TaskStatus: Open, Active, Done
/// </summary>
public enum TaskStatus {
    Open,
    Active,
    Done
}

/// <summary>
/// Task entity class for EF
/// </summary>
public class TaskEntity {
    public int Id {get;set;}
    public TaskStatus Status {get;set;}
    public string Title {get;set;}
    public string Description {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime UpdatedAt {get;set;}
}
