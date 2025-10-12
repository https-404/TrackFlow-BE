namespace TrackFlow.Domain.Entities;

public class Profile: BaseEntity
{
    // Use Id from BaseEntity
    public User user { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    //media links
    public string ProfilePhotoURL { get; set; }
    public string CoverPhotoURL { get; set; }
    
    public string bio { get; set; }
    public string profileDescription { get; set; }
    
    
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    
}