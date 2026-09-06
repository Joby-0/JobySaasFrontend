using JobySaasFrontend.Models;

namespace JobySaasFrontend.Services;

public class AlertService
{
    public event Action? OnChange;

    private readonly List<AlertMessage> _alerts = new();

    public IReadOnlyList<AlertMessage> Alerts => _alerts;

    public void Show(string message, AlertType type = AlertType.Info, bool autoHide = true, int duration = 5000)
    {
        var alert = new AlertMessage
        {
            Message = message,
            Type = type,
            AutoHide = autoHide,
            Duration = duration
        };

        _alerts.Add(alert);

        OnChange?.Invoke();
    }

    public void Remove(Guid id)
    {
        var alert = _alerts.FirstOrDefault(x => x.Id == id);

        if (alert is null)
            return;

        _alerts.Remove(alert);

        OnChange?.Invoke();
    }

    public void Clear()
    {
        _alerts.Clear();

        OnChange?.Invoke();
    }
}