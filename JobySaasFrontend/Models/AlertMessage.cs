namespace JobySaasFrontend.Models;

public class AlertMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Message { get; set; } = string.Empty;

    public AlertType Type { get; set; }

    public bool AutoHide { get; set; } = true;

    public int Duration { get; set; } = 5000;
}

public enum AlertType
{
    Success,
    Warning,
    Info,
    Error
}