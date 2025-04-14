public enum TaskStatus {
    Open,
    Active,
    Done
}

public class TaskEntity {
    public int Id {get;set;}
    public TaskStatus Status {get;set;}
    public string Title {get;set;}
    public string Description {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime UpdatedAt {get;set;}
}
