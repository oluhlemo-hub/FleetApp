namespace Fleet.Client.Services;

public class ThemeService
{
    public string AccentColor { get; private set; } = "#0d6efd";
    public int ServiceInterval { get; private set; } = 1000;
    public string TenantName { get; private set; } = "My Fleet Company";

    // Added ? to handle the nullability warning
    public event Action? OnChange;

    public void UpdateSettings(string color, int interval, string name)
    {
        AccentColor = color;
        ServiceInterval = interval;
        TenantName = name;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}