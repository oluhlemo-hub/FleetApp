using Postgrest.Attributes;
using Postgrest.Models;

namespace FleetManagement.Models;

[Table("tenant_settings")]
public class TenantSettings : BaseModel
{
    [PrimaryKey("id")] public string Id { get; set; } = string.Empty;
    [Column("company_name")] public string? CompanyName { get; set; }
    [Column("support_email")] public string? SupportEmail { get; set; }
    [Column("logo_url")] public string? LogoUrl { get; set; }
    [Column("maintenance_alerts")] public bool MaintenanceAlerts { get; set; }
    [Column("fuel_alerts")] public bool FuelAlerts { get; set; }
    [Column("doc_expiry_alerts")] public bool DocExpiryAlerts { get; set; }
    [Column("trip_alerts")] public bool TripAlerts { get; set; }
}