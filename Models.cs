using Postgrest.Attributes;
using Postgrest.Models;

namespace FleetManagement.Models
{
    [Table("vehicles")]
    public class Vehicle : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("make")]
        public string Make { get; set; } = "";

        [Column("model")]
        public string Model { get; set; } = "";

        [Column("registration")]
        public string Registration { get; set; } = "";

        [Column("mileage")]
        public int Mileage { get; set; }

        [Column("service_due_km")]
        public int ServiceDueKm { get; set; }

        [Column("status")]
        public string Status { get; set; } = "Available";

        [Column("last_driver")]
        public string? LastDriver { get; set; }
    }

    [Table("drivers")]
    public class Driver : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("auth_id")]
        public string AuthId { get; set; } = "";

        [Column("full_name")]
        public string Name { get; set; } = "";

        [Column("email")]
        public string Email { get; set; } = "";

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("role")]
        public string Role { get; set; } = "Driver";

        [Column("is_active")]
        public bool IsActive { get; set; } = true;
    }

    [Table("settings")]
    public class AppSettings : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("theme")]
        public string Theme { get; set; } = "Default (Blue)";

        [Column("email_alerts")]
        public bool EmailAlerts { get; set; } = true;

        [Column("sms_alerts")]
        public bool SmsAlerts { get; set; } = false;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}