namespace TrackFlow.Domain.Entities;

public class Profile: BaseEntity
{
    // Use Id from BaseEntity
    public Guid UserId { get; set; }
    public required User User { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    //media links
    public string ProfilePhotoURL { get; set; } = string.Empty;
    public string CoverPhotoURL { get; set; } = string.Empty;
    
    public string bio { get; set; } = string.Empty;
    public string profileDescription { get; set; } = string.Empty;
    
    
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    
}