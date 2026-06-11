using Microsoft.AspNetCore.Components;

namespace FleetManagement.Services
{
    public class TourStep
    {
        public string ElementId { get; set; } = "";
        public string Title     { get; set; } = "";
        public string Body      { get; set; } = "";
        public string Position  { get; set; } = "bottom";
    }

    public class TourService
    {
        public bool IsActive     { get; private set; } = false;
        public int  CurrentIndex { get; private set; } = 0;
        public List<TourStep> Steps { get; private set; } = new();

        public event Action? OnChanged;
        public event Action? OnAdminTourRequested;

        public TourStep? CurrentStep => IsActive && CurrentIndex < Steps.Count ? Steps[CurrentIndex] : null;
        public int  TotalSteps => Steps.Count;
        public bool IsLastStep => CurrentIndex == Steps.Count - 1;

        private static readonly List<TourStep> AdminSteps = new()
        {
            new() { ElementId = "tour-dashboard-tab",  Title = "Dashboard Overview",   Body = "Your main control panel — vehicles, drivers, faults, and fuel logs at a glance.",           Position = "bottom" },
            new() { ElementId = "tour-vehicles-tab",   Title = "Vehicles",              Body = "Manage your fleet — add vehicles, track mileage, and monitor service due dates.",            Position = "bottom" },
            new() { ElementId = "tour-drivers-tab",    Title = "Drivers",               Body = "Add and manage drivers. Set roles, view ratings, activate or deactivate accounts.",          Position = "bottom" },
            new() { ElementId = "tour-faults-tab",     Title = "Fault Reports",         Body = "Drivers report vehicle faults here. View, investigate, and resolve them.",                   Position = "bottom" },
            new() { ElementId = "tour-fuellogs-tab",   Title = "Fuel & Expenses",       Body = "Track all fuel fill-ups and vehicle expenses submitted by drivers.",                         Position = "bottom" },
            new() { ElementId = "tour-settings-link",  Title = "Settings",              Body = "Configure company details, notifications, DVIR checklists, and more.",                       Position = "right"  },
            new() { ElementId = "tour-profile-btn",    Title = "Your Profile",          Body = "Update your profile picture and settings. Retake this tour anytime from here.",              Position = "left"   },
        };

        private static readonly List<TourStep> DriverSteps = new()
        {
            new() { ElementId = "tour-vehicle-section", Title = "Your Assigned Vehicle", Body = "Your currently assigned vehicle — registration, make, model, and mileage.",                Position = "bottom" },
            new() { ElementId = "tour-request-btn",     Title = "Request a Vehicle",     Body = "Need a different vehicle? Submit a request to your fleet admin here.",                      Position = "bottom" },
            new() { ElementId = "tour-trip-btn",        Title = "Start a Trip",          Body = "Before every trip, log your start mileage and complete the pre-trip DVIR checklist.",       Position = "bottom" },
            new() { ElementId = "tour-fuel-btn",        Title = "Log Fuel & Expenses",   Body = "Record every fuel fill-up or vehicle expense here. Attach a receipt photo.",                Position = "bottom" },
            new() { ElementId = "tour-fault-btn",       Title = "Report a Fault",        Body = "Notice something wrong with the vehicle? Report it here immediately.",                      Position = "bottom" },
            new() { ElementId = "tour-profile-btn",     Title = "Your Profile",          Body = "View your profile, change your picture, and retake this tour anytime.",                     Position = "left"   },
        };

        public void StartAdminTour()  { Steps = AdminSteps;  CurrentIndex = 0; IsActive = true;  OnChanged?.Invoke(); }
        public void RequestAdminTour()   { OnAdminTourRequested?.Invoke(); }
        public void StartDriverTour() { Steps = DriverSteps; CurrentIndex = 0; IsActive = true;  OnChanged?.Invoke(); }

        public void Next()
        {
            if (CurrentIndex < Steps.Count - 1) CurrentIndex++;
            else End();
            OnChanged?.Invoke();
        }

        public void Previous() { if (CurrentIndex > 0) CurrentIndex--; OnChanged?.Invoke(); }

        public void End() { IsActive = false; CurrentIndex = 0; Steps = new(); OnChanged?.Invoke(); }
    }
}
