using TrackFlow.Application.Common;
using TrackFlow.Application.DTOs.Auth;
using TrackFlow.Domain.Entities;

namespace TrackFlow.Application.Services.Interface;

public interface IAuthService
{
    public Task<ApiResponse<AuthResponse?>> LoginAsync(LoginDTO login);
    
    public Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterDTO register);
    
    public Task<string> GenerateToken(User user);

}