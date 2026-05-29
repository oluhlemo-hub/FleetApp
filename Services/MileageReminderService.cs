using FleetManagement.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FleetManagement.Services;

public class MileageReminderService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<MileageReminderService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromHours(12);

    public MileageReminderService(IServiceProvider services, ILogger<MileageReminderService> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckAndSendReminders();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MileageReminderService");
            }
            await Task.Delay(_interval, stoppingToken);
        }
    }

    private async Task CheckAndSendReminders()
    {
        _logger.LogInformation("MileageReminderService: Starting check at {Time}", DateTime.UtcNow);
        using var scope = _services.CreateScope();
        var supabase = scope.ServiceProvider.GetRequiredService<Supabase.Client>();

        var apiKey = Environment.GetEnvironmentVariable("RESEND_API_KEY") ?? "";
        var adminEmail = Environment.GetEnvironmentVariable("EMAIL_ADMIN") ?? "";
        var cutoff = DateTime.UtcNow.AddHours(-12);

        // Get all active vehicles with an assigned driver
        var vehiclesResult = await supabase.From<Vehicle>()
            .Filter("status", Postgrest.Constants.Operator.Equals, "Active")
            .Get();

        var activeVehicles = vehiclesResult.Models
            .Where(v => !string.IsNullOrEmpty(v.LastDriver))
            .ToList();
        _logger.LogInformation("MileageReminderService: Found {Count} active vehicles with drivers", activeVehicles.Count);

        // Get all active drivers
        var driversResult = await supabase.From<Driver>()
            .Filter("is_active", Postgrest.Constants.Operator.Equals, "true")
            .Get();
        var drivers = driversResult.Models;

        // Get recent trips (last 12 hours)
        var tripsResult = await supabase.From<TripHistory>().Get();
        var recentTrips = tripsResult.Models
            .Where(t => t.CreatedAt >= cutoff)
            .Select(t => t.DriverEmail)
            .ToHashSet();

        foreach (var vehicle in activeVehicles)
        {
            var driver = drivers.FirstOrDefault(d => d.Name == vehicle.LastDriver || d.Email == vehicle.LastDriver);
            if (driver == null) continue;

            _logger.LogInformation("MileageReminderService: Checking driver {Email}, recent trip: {HasTrip}", driver.Email, recentTrips.Contains(driver.Email));
            // TESTING: skip trip check
            // if (recentTrips.Contains(driver.Email)) continue;

            _logger.LogInformation("MileageReminderService: Sending reminder to {Email} for vehicle {Reg}", driver.Email, vehicle.Registration);

            // Send both driver reminder and admin alert to admin email
            await SendReminderEmail(apiKey, adminEmail, driver.Name, vehicle.Registration, vehicle.Mileage);
            await SendAdminAlertEmail(apiKey, adminEmail, driver.Name, driver.Email, vehicle.Registration, vehicle.Mileage);
        }
    }

    private async Task SendReminderEmail(string apiKey, string toEmail, string driverName, string vehicleReg, int mileage)
    {
        var payload = new
        {
            from = "onboarding@resend.dev",
            to = new[] { toEmail },
            subject = $"⚠️ Mileage Update Reminder - {vehicleReg}",
            html = $@"
                <h2>⚠️ Mileage Update Reminder</h2>
                <p>Hi {driverName},</p>
                <p>You have not logged a trip or updated your mileage in the last 12 hours.</p>
                <p><strong>Vehicle:</strong> {vehicleReg}</p>
                <p><strong>Last recorded mileage:</strong> {mileage} km</p>
                <p>Please log into the Fleet Management system and update your mileage.</p>
                <hr/>
                <p style='color:gray;font-size:12px'>Sent automatically by Fleet Management System</p>"
        };
        await PostToResend(apiKey, payload);
    }

    private async Task SendAdminAlertEmail(string apiKey, string adminEmail, string driverName, string driverEmail, string vehicleReg, int mileage)
    {
        var payload = new
        {
            from = "onboarding@resend.dev",
            to = new[] { adminEmail },
            subject = $"🚨 Driver Mileage Not Updated - {driverName}",
            html = $@"
                <h2>🚨 Driver Has Not Updated Mileage</h2>
                <p><strong>Driver:</strong> {driverName} ({driverEmail})</p>
                <p><strong>Vehicle:</strong> {vehicleReg}</p>
                <p><strong>Last recorded mileage:</strong> {mileage} km</p>
                <p>This driver has not logged a trip in the last 12 hours.</p>
                <hr/>
                <p style='color:gray;font-size:12px'>Sent automatically by Fleet Management System</p>"
        };
        await PostToResend(apiKey, payload);
    }

    private async Task PostToResend(string apiKey, object payload)
    {
        using var http = new HttpClient();
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await http.PostAsync("https://api.resend.com/emails", content);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            _logger.LogError("Resend error: {Body}", body);
        }
    }
}
