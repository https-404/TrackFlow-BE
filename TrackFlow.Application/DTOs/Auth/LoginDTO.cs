using System.ComponentModel.DataAnnotations;

namespace TrackFlow.Application.DTOs.Auth;

public class LoginDTO
{
    [Required]
    public string email { get; set; }
    [Required]
    public string password { get; set; }
}