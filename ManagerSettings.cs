using Postgrest.Attributes;
using Postgrest.Models;

namespace FleetManagement.Models;

[Table("manager_settings")]
public class ManagerSettings : BaseModel
{
    [PrimaryKey("id")] public string Id { get; set; } = string.Empty;
    [Column("user_id")] public string UserId { get; set; } = string.Empty;
    [Column("trip_approval_mode")] public string? TripApprovalMode { get; set; }
    [Column("fuel_approval_mode")] public string? FuelApprovalMode { get; set; }
}