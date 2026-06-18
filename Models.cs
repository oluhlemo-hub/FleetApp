using Postgrest.Attributes;
using Postgrest.Models;

namespace FleetManagement.Models
{
    [Table("vehicles")]
    public class Vehicle : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
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

        [Column("mileage_updated_at")]
        public DateTime? MileageUpdatedAt { get; set; }
        [Column("service_due_km")]
        public int ServiceDueKm { get; set; }
        [Column("last_driver")]
        public string? LastDriver { get; set; }
    }

    [Table("drivers")]
    public class Driver : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("full_name")]
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
        [Column("total_km_driven")]
        public int TotalKmDriven { get; set; }
        [Column("last_vehicle")]
        public string? LastVehicle { get; set; }
        [Column("profile_picture_url")]
        public string? ProfilePictureUrl { get; set; }
        [Column("has_completed_setup")]
        public bool HasCompletedSetup { get; set; } = false;
        [Column("has_taken_tour")]
        public bool HasTakenTour { get; set; } = false;
    }

    [Table("faults")]
    public class Fault : BaseModel
    {
        [PrimaryKey("id", true)]
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
        [Column("attachment_url")]
        public string? AttachmentUrl { get; set; }
        [Column("acknowledged_at")]
        public DateTime? AcknowledgedAt { get; set; }
        [Column("assigned_to")]
        public string? AssignedTo { get; set; }
        [Column("expected_resolution_date")]
        public DateTime? ExpectedResolutionDate { get; set; }
        [Column("repair_booking_id")]
        public string? RepairBookingId { get; set; }
    }

    [Table("repair_bookings")]
    public class RepairBooking : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("fault_id")]
        public string FaultId { get; set; } = string.Empty;
        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;
        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = string.Empty;
        [Column("driver_email")]
        public string DriverEmail { get; set; } = string.Empty;
        [Column("garage_name")]
        public string GarageName { get; set; } = string.Empty;
        [Column("garage_contact")]
        public string? GarageContact { get; set; }
        [Column("booking_date")]
        public DateTime BookingDate { get; set; }
        [Column("expected_completion_date")]
        public DateTime? ExpectedCompletionDate { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("quote_url")]
        public string? QuoteUrl { get; set; }
        [Column("collected_at")]
        public DateTime? CollectedAt { get; set; }
        [Column("collection_notes")]
        public string? CollectionNotes { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("service_history")]
    public class ServiceHistory : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
        [PrimaryKey("id", true)]
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
    }

    [Table("fuel_logs")]
    public class FuelLog : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
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


    
    [Table("vehicle_documents")]
    public class VehicleDocument : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;
        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = string.Empty;
        [Column("document_type")]
        public string DocumentType { get; set; } = string.Empty;
        [Column("file_url")]
        public string? FileUrl { get; set; }
        [Column("expiry_date")]
        public DateTime ExpiryDate { get; set; }
        [Column("uploaded_by")]
        public string UploadedBy { get; set; } = string.Empty;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("dvir_checklist_items")]
    public class DvirChecklistItem : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("category")]
        public string Category { get; set; } = "General";
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
        [Column("sort_order")]
        public int SortOrder { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("dvir_submissions")]
    public class DvirSubmission : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("driver_email")]
        public string DriverEmail { get; set; } = string.Empty;
        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;
        [Column("vehicle_registration")]
        public string VehicleRegistration { get; set; } = string.Empty;
        [Column("trigger_type")]
        public string TriggerType { get; set; } = string.Empty;
        [Column("has_failures")]
        public bool HasFailures { get; set; }
        [Column("notes")]
        public string? Notes { get; set; }
        [Column("submitted_at")]
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("dvir_submission_items")]
    public class DvirSubmissionItem : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("submission_id")]
        public string SubmissionId { get; set; } = string.Empty;
        [Column("item_id")]
        public string ItemId { get; set; } = string.Empty;
        [Column("item_name")]
        public string ItemName { get; set; } = string.Empty;
        [Column("result")]
        public string Result { get; set; } = "pass";
        [Column("notes")]
        public string? Notes { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    [Table("profiles")]
    public class UserProfile : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("role")]
        public string? Role { get; set; }
    }

    [Table("user_settings")]
    public class UserSettings : BaseModel
    {
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
        [PrimaryKey("id", true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        [Column("trip_approval_mode")]
        public string? TripApprovalMode { get; set; }
        [Column("fuel_approval_mode")]
        public string? FuelApprovalMode { get; set; }
    }
}
