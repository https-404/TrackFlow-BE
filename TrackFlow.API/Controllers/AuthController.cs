using Microsoft.AspNetCore.Mvc;
using TrackFlow.Application.Common;
using TrackFlow.Application.DTOs.Auth;
using TrackFlow.Application.Services.Interface;

namespace TrackFlow.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("signup")]
    public async Task<ApiResponse<AuthResponse>> Signup([FromBody] RegisterDTO registerDto)
    {
        return await _authService.RegisterAsync(registerDto);
    }

    [HttpPost("signin")]
    public async Task<ApiResponse<AuthResponse?>> SignIn([FromBody] LoginDTO body)
    {
        return await _authService.LoginAsync(body);
    }
}