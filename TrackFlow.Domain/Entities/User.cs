namespace TrackFlow.Domain.Entities;

public class User : BaseEntity
{
    // Use Id from BaseEntity
    
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Profile? Profile { get; set; }
    public ICollection<UserOrganization> UserOrganizations { get; set; } = new List<UserOrganization>();

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}