
public enum TaskStatus {
    Open,
    Active,
    Done
}

public record TaskNew(
    int Id,
    string Title,
    string Description,
    DateTime? CreatedAt,
    DateTime? Timestamp,
    TaskStatus Status,
    Guid logId,
    string Action = "New"
);

public record TaskEdit(
    int Id,
    string Title,
    string Description,
    DateTime? CreatedAt,
    DateTime? Timestamp,
    TaskStatus Status,
    Guid logId,
    string Action = "Edit"
);

public record TaskDelete(
    int Id,
    DateTime? Timestamp,
    Guid logId,
    string Action = "Delete"
);


