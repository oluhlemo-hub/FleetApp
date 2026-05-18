using Postgrest.Attributes;
using Postgrest.Models;
using System;

namespace FleetManagement.Models
{
    [Table("vehicle_requests")]
    public class VehicleRequest : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Column("driver_email")]
        public string DriverEmail { get; set; } = string.Empty;

        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;

        [Column("mileage")]
        public int Mileage { get; set; }

        [Column("admin_email")]
        public string AdminEmail { get; set; } = string.Empty;

        [Column("otp_code")]
        public string OtpCode { get; set; } = string.Empty;

        [Column("status")]
        public string Status { get; set; } = "Pending";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}