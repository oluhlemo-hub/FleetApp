using Postgrest.Attributes;
using Postgrest.Models;

namespace FleetManagement.Models
{
    [Table("faults")]
    public class Fault : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;

        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = string.Empty;

        [Column("driver_email")]
        public string DriverEmail { get; set; } = string.Empty;

        [Column("fault_type")]
        public string FaultType { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("status")]
        public string Status { get; set; } = "Open";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}