using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FleetManagement.Services;

public class EmailService
{
    public async Task SendServiceReportAsync(string vehicleReg, string driverEmail, int mileage, string reportUrl)
    {
        var apiKey = Environment.GetEnvironmentVariable("RESEND_API_KEY") ?? "";
        var adminEmail = Environment.GetEnvironmentVariable("EMAIL_ADMIN") ?? "oluhlemo@gmail.com";

        if (string.IsNullOrEmpty(apiKey))
            throw new Exception("RESEND_API_KEY environment variable is not set.");

        var payload = new
        {
            from = "onboarding@resend.dev",
            to = new[] { adminEmail },
            subject = $"Service Report - {vehicleReg} collected",
            html = $@"
                <h2>Vehicle Collected from Service</h2>
                <p><strong>Vehicle:</strong> {vehicleReg}</p>
                <p><strong>Driver:</strong> {driverEmail}</p>
                <p><strong>Mileage at Service:</strong> {mileage} km</p>
                <p><strong>Next Service Due:</strong> {mileage + 10000} km</p>
                <p><strong>Service Report:</strong> <a href='{reportUrl}'>View Report</a></p>
                <hr/>
                <p style='color:gray;font-size:12px'>Sent automatically by Fleet Management System</p>"
        };

        using var http = new HttpClient();
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await http.PostAsync("https://api.resend.com/emails", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Resend API error {response.StatusCode}: {responseBody}");
    }
}
