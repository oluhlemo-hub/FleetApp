using Postgrest.Attributes;
using Postgrest.Models;

namespace FleetManagement.Models;

[Table("user_settings")]
public class UserSettings : BaseModel
{
    [PrimaryKey("id")] public string Id { get; set; } = string.Empty;
    [Column("user_id")] public string UserId { get; set; } = string.Empty;
    [Column("theme")] public string? Theme { get; set; }
    [Column("accent_colour")] public string? AccentColour { get; set; }
    [Column("notify_by_email")] public bool NotifyByEmail { get; set; }
    [Column("notify_by_sms")] public bool NotifyBySMS { get; set; }
    [Column("notify_by_push")] public bool NotifyByPush { get; set; }
}