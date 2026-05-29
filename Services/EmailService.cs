using System.Net;
using System.Net.Mail;

namespace FleetManagement.Services;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendServiceReportAsync(string vehicleReg, string driverEmail, int mileage, string reportUrl)
    {
        var host = Environment.GetEnvironmentVariable("EMAIL_SMTP_HOST")
                   ?? _config["Email:SmtpHost"] ?? "smtp.gmail.com";
        var port = int.Parse(Environment.GetEnvironmentVariable("EMAIL_SMTP_PORT")
                   ?? _config["Email:SmtpPort"] ?? "587");
        var sender = Environment.GetEnvironmentVariable("EMAIL_SENDER")
                     ?? _config["Email:SenderEmail"]!;
        var password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
                       ?? _config["Email:SenderPassword"]!;
        var adminEmail = Environment.GetEnvironmentVariable("EMAIL_ADMIN")
                         ?? _config["Email:AdminEmail"]!;

        using var client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(sender, password),
            EnableSsl = true
        };

        var body = $@"
            <h2>🔧 Vehicle Collected from Service</h2>
            <p><strong>Vehicle:</strong> {vehicleReg}</p>
            <p><strong>Driver:</strong> {driverEmail}</p>
            <p><strong>Mileage at Service:</strong> {mileage} km</p>
            <p><strong>Next Service Due:</strong> {mileage + 10000} km</p>
            <p><strong>Service Report:</strong> <a href='{reportUrl}'>View Report</a></p>
            <hr/>
            <p style='color:gray;font-size:12px'>Sent automatically by Fleet Management System</p>";

        var message = new MailMessage
        {
            From = new MailAddress(sender, "Fleet Management"),
            Subject = $"Service Report - {vehicleReg} collected",
            Body = body,
            IsBodyHtml = true
        };
        message.To.Add(adminEmail);

        await client.SendMailAsync(message);
    }
}
