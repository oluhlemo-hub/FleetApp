using Postgrest.Attributes;
using Postgrest.Models;

namespace FleetManagement.Models
{
    [Table("app_settings")]
    public class AppSettings : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("company_name")]
        public string CompanyName { get; set; } = "My Fleet Company";

        [Column("support_email")]
        public string SupportEmail { get; set; } = "";

        [Column("service_interval_km")]
        public int ServiceIntervalKm { get; set; } = 1000;

        [Column("default_status")]
        public string DefaultStatus { get; set; } = "Active";

        [Column("selected_color")]
        public string SelectedColor { get; set; } = "#0d6efd";

        [Column("logo_url")]
        public string LogoUrl { get; set; } = "";

        [Column("notify_on_service_due")]
        public bool NotifyOnServiceDue { get; set; } = true;

        [Column("notify_on_driver_assigned")]
        public bool NotifyOnDriverAssigned { get; set; } = false;

        [Column("notify_driver_on_service_due")]
        public bool NotifyDriverOnServiceDue { get; set; } = false;
    }
}