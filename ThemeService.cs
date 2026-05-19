using System;
using System.Threading.Tasks;

namespace FleetManagement.Services
{
    public class ThemeService
    {
        private string _currentTheme = "Light";

        public string CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    OnThemeChanged?.Invoke();
                }
            }
        }

        public string ThemeClass => _currentTheme.ToLower() == "dark" ? "theme-dark" : "theme-light";

        public event Action? OnThemeChanged;

        public Task SetThemeAsync(string theme)
        {
            CurrentTheme = theme;
            return Task.CompletedTask;
        }
    }
}