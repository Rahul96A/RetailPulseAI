using RetailPulseAI.Domain.Common;
using RetailPulseAI.Domain.Enums;

namespace RetailPulseAI.Domain.Entities;

public sealed class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Viewer;
}
