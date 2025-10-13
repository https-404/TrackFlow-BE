using System.ComponentModel.DataAnnotations;

namespace TrackFlow.Application.DTOs.Auth;

public class RegisterDTO
{
    [Required]
    [EmailAddress]
    public string email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string password { get; set; }

}