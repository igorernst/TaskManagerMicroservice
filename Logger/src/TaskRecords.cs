
public enum TaskStatus {
    Open,
    Active,
    Done
}

record TaskNew(
    int Id,
    string Title,
    string Description,
    DateTime? CreatedAt,
    DateTime? Timestamp,
    TaskStatus Status,
    Guid logId,
    string Action = "New"
);

record TaskEdit(
    int Id,
    string Title,
    string Description,
    DateTime? CreatedAt,
    DateTime? Timestamp,
    TaskStatus Status,
    Guid logId,
    string Action = "Edit"
);

record TaskDelete(
    int Id,
    DateTime? Timestamp,
    Guid logId,
    string Action = "Delete"
);


