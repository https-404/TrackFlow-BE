namespace TrackFlow.Domain.Entities;

public class UserOrganization : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public UserRole Role { get; set; } = UserRole.Member;
}
