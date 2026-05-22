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

        [Column("last_service_date")]
        public DateTime? LastServiceDate { get; set; }

        [Column("last_service_km")]
        public int? LastServiceKm { get; set; }

        [Column("service_report_url")]
        public string? ServiceReportUrl { get; set; }
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

        [Column("must_change_password")]
        public bool MustChangePassword { get; set; } = true;

        [Column("total_km_driven")]
        public int TotalKmDriven { get; set; } = 0;

        [Column("last_vehicle")]
        public string? LastVehicle { get; set; }
    }

    [Table("faults")]
    public class Fault : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("vehicle_id")]
        public string VehicleId { get; set; } = "";

        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = "";

        [Column("driver_email")]
        public string DriverEmail { get; set; } = "";

        [Column("fault_type")]
        public string FaultType { get; set; } = "";

        [Column("description")]
        public string Description { get; set; } = "";

        [Column("status")]
        public string Status { get; set; } = "Open";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("vehicle_requests")]
    public class VehicleRequest : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("driver_email")]
        public string DriverEmail { get; set; } = "";

        [Column("vehicle_id")]
        public string VehicleId { get; set; } = "";

        [Column("mileage")]
        public int Mileage { get; set; }

        [Column("admin_email")]
        public string AdminEmail { get; set; } = "";

        [Column("otp_code")]
        public string OtpCode { get; set; } = "";

        [Column("status")]
        public string Status { get; set; } = "Pending";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("user_settings")]
    public class UserSettings : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("user_id")]
        public string UserId { get; set; } = "";

        [Column("theme")]
        public string? Theme { get; set; }

        [Column("accent_colour")]
        public string? AccentColour { get; set; }

        [Column("notify_by_email")]
        public bool NotifyByEmail { get; set; }

        [Column("notify_by_sms")]
        public bool NotifyBySMS { get; set; }

        [Column("notify_by_push")]
        public bool NotifyByPush { get; set; }
    }

    [Table("tenant_settings")]
    public class TenantSettings : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("company_name")]
        public string? CompanyName { get; set; }

        [Column("support_email")]
        public string? SupportEmail { get; set; }

        [Column("logo_url")]
        public string? LogoUrl { get; set; }

        [Column("maintenance_alerts")]
        public bool MaintenanceAlerts { get; set; }

        [Column("fuel_alerts")]
        public bool FuelAlerts { get; set; }

        [Column("doc_expiry_alerts")]
        public bool DocExpiryAlerts { get; set; }

        [Column("trip_alerts")]
        public bool TripAlerts { get; set; }
    }

    [Table("manager_settings")]
    public class ManagerSettings : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("user_id")]
        public string UserId { get; set; } = "";

        [Column("trip_approval_mode")]
        public string? TripApprovalMode { get; set; }

        [Column("fuel_approval_mode")]
        public string? FuelApprovalMode { get; set; }
    }

    [Table("profiles")]
    public class UserProfile : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("full_name")]
        public string? FullName { get; set; }

        [Column("role")]
        public string? Role { get; set; }

        [Column("email")]
        public string? Email { get; set; }
    }

    [Table("service_history")]
    public class ServiceHistory : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("vehicle_id")]
        public string VehicleId { get; set; } = "";

        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = "";

        [Column("driver_email")]
        public string DriverEmail { get; set; } = "";

        [Column("service_station")]
        public string? ServiceStation { get; set; }

        [Column("mileage_at_service")]
        public int MileageAtService { get; set; }

        [Column("next_service_km")]
        public int NextServiceKm { get; set; }

        [Column("report_url")]
        public string? ReportUrl { get; set; }

        [Column("serviced_at")]
        public DateTime ServicedAt { get; set; } = DateTime.UtcNow;
    }
}
    [Table("trip_history")]
    public class TripHistory : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = "";

        [Column("vehicle_id")]
        public string VehicleId { get; set; } = "";

        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = "";

        [Column("driver_email")]
        public string DriverEmail { get; set; } = "";

        [Column("driver_name")]
        public string DriverName { get; set; } = "";

        [Column("start_mileage")]
        public int StartMileage { get; set; }

        [Column("end_mileage")]
        public int EndMileage { get; set; }

        [Column("return_condition")]
        public string? ReturnCondition { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
