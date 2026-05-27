using Postgrest.Attributes;
using Postgrest.Models;

namespace FleetManagement.Models
{
    [Table("vehicles")]
    public class Vehicle : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
        [Column("make")]
        public string Make { get; set; } = string.Empty;
        [Column("model")]
        public string Model { get; set; } = string.Empty;
        [Column("registration")]
        public string Registration { get; set; } = string.Empty;
        [Column("status")]
        public string Status { get; set; } = "Available";
        [Column("mileage")]
        public int Mileage { get; set; }
        [Column("service_due_km")]
        public int ServiceDueKm { get; set; }
        [Column("last_driver")]
        public string? LastDriver { get; set; }
    }

    [Table("drivers")]
    public class Driver : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("phone")]
        public string? Phone { get; set; }
        [Column("role")]
        public string Role { get; set; } = "Driver";
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
        [Column("auth_id")]
        public string? AuthId { get; set; }
        [Column("must_change_password")]
        public bool MustChangePassword { get; set; }
        [Column("fuel_card_number")]
        public string? FuelCardNumber { get; set; }
    }

    [Table("faults")]
    public class Fault : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
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

    [Table("service_history")]
    public class ServiceHistory : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;
        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = string.Empty;
        [Column("driver_email")]
        public string DriverEmail { get; set; } = string.Empty;
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

    [Table("trip_history")]
    public class TripHistory : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;
        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = string.Empty;
        [Column("driver_email")]
        public string DriverEmail { get; set; } = string.Empty;
        [Column("driver_name")]
        public string DriverName { get; set; } = string.Empty;
        [Column("start_mileage")]
        public int StartMileage { get; set; }
        [Column("end_mileage")]
        public int EndMileage { get; set; }
        [Column("return_condition")]
        public string? ReturnCondition { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("vehicle_requests")]
    public class VehicleRequest : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
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
    }

    [Table("fuel_logs")]
    public class FuelLog : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;
        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = string.Empty;
        [Column("driver_email")]
        public string DriverEmail { get; set; } = string.Empty;
        [Column("expense_type")]
        public string ExpenseType { get; set; } = "Fuel";
        [Column("fuel_type")]
        public string? FuelType { get; set; }
        [Column("litres")]
        public decimal? Litres { get; set; }
        [Column("cost")]
        public decimal Cost { get; set; }
        [Column("mileage_km")]
        public int? MileageKm { get; set; }
        [Column("station_name")]
        public string? StationName { get; set; }
        [Column("expense_category")]
        public string? ExpenseCategory { get; set; }
        [Column("expense_description")]
        public string? ExpenseDescription { get; set; }
        [Column("receipt_url")]
        public string? ReceiptUrl { get; set; }
        [Column("fuel_card_number")]
        public string? FuelCardNumber { get; set; }
        [Column("logged_at")]
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("fuel_cards")]
    public class FuelCard : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
        [Column("card_number")]
        public string CardNumber { get; set; } = string.Empty;
        [Column("provider")]
        public string? Provider { get; set; }
        [Column("assigned_driver_email")]
        public string? AssignedDriverEmail { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("profiles")]
    public class UserProfile : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
        [Column("role")]
        public string? Role { get; set; }
    }

    [Table("user_settings")]
    public class UserSettings : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
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
        public string Id { get; set; } = string.Empty;
        [Column("company_name")]
        public string? CompanyName { get; set; }
        [Column("support_email")]
        public string? SupportEmail { get; set; }
        [Column("logo_url")]
        public string? LogoUrl { get; set; }
        [Column("maintenance_alerts")]
        public bool MaintenanceAlerts { get; set; }
        [Column("doc_expiry_alerts")]
        public bool DocExpiryAlerts { get; set; }
        [Column("trip_alerts")]
        public bool TripAlerts { get; set; }
        [Column("fuel_alerts")]
        public bool FuelAlerts { get; set; }
    }

    [Table("manager_settings")]
    public class ManagerSettings : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        [Column("trip_approval_mode")]
        public string? TripApprovalMode { get; set; }
        [Column("fuel_approval_mode")]
        public string? FuelApprovalMode { get; set; }
    }
}
