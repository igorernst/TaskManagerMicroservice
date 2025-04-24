using Microsoft.Extensions.Options;

public interface ILoggingService {
    Task PostLogTaskNewAsync(TaskNew task);
    Task PostLogTaskEditAsync(TaskEdit task);
    Task PostLogTaskDeleteAsync(TaskDelete task);
}

public record TaskNew(
    int Id,
    string Title,
    string Description,
    DateTime? CreatedAt,
    TaskStatus Status
);

public record TaskEdit(
    int Id,
    string Title,
    string Description,
    DateTime? CreatedAt,
    TaskStatus Status
);

public record TaskDelete(
    int Id
);

/// <summary>
/// Setting for Logging API
/// </summary>
public class LoggingApiSettings
{
    public string BaseUrl { get; set; }
    public Endpoints Endpoints { get; set; }
}

public class Endpoints
{
    public string LogTaskNew { get; set; }
    public string LogTaskEdit { get; set; }
    public string LogTaskDelete { get; set; }
}

/// <summary>
/// HTTP client for Logging Service API
/// </summary>
public class LoggingService : ILoggingService
{
    private readonly HttpClient _httpClient;
    private readonly LoggingApiSettings _settings;
    public LoggingService(HttpClient httpClient, IOptions<LoggingApiSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    /// <summary>
    /// Sends log after a new task is created
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public async Task PostLogTaskNewAsync(TaskNew task) {
        var response = await _httpClient.PostAsJsonAsync(_settings.BaseUrl + _settings.Endpoints.LogTaskNew, task);
    }

    /// <summary>
    /// Sends log after a task is edited
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public async Task PostLogTaskEditAsync(TaskEdit task) {
        var response = await _httpClient.PostAsJsonAsync(_settings.BaseUrl + _settings.Endpoints.LogTaskEdit, task);
    }

    /// <summary>
    /// Sends log after a task is deleted
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public async Task PostLogTaskDeleteAsync(TaskDelete task) {
        var response = await _httpClient.PostAsJsonAsync(_settings.BaseUrl + _settings.Endpoints.LogTaskDelete, task);
    }

}
