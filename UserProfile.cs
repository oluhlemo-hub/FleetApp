using Postgrest.Attributes;
using Postgrest.Models;

namespace FleetManagement.Models;

[Table("profiles")]
public class UserProfile : BaseModel
{
    [PrimaryKey("id")] public string Id { get; set; } = string.Empty;
    [Column("role")] public string? Role { get; set; }
    [Column("full_name")] public string? FullName { get; set; }
}