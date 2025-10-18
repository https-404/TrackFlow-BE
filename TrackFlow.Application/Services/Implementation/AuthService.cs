using System;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using TrackFlow.Application.Common;
using TrackFlow.Application.DTOs.Auth;
using TrackFlow.Application.Services.Interface;
using TrackFlow.Domain.Entities;
using TrackFlow.Domain.Interfaces.IRepository;

namespace TrackFlow.Application.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthService(AppDbContext appDbContext, IConfiguration configuration, IUserRepository userRepository)
    {
        _appDbContext = appDbContext;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<AuthResponse?>> LoginAsync(LoginDTO login)
    {
        var user = await _userRepository.GetByEmailAsync(login.email);
        if (user == null)
        {
            return new ApiResponse<AuthResponse?>("User not found", 404, null);
        }

        // Verify password using ASP.NET Core Identity's PasswordHasher
        var hasher = new PasswordHasher<User>();
        var verifyResult = hasher.VerifyHashedPassword(user, user.PasswordHash, login.password);
        if (verifyResult == PasswordVerificationResult.Failed)
        {
            return new ApiResponse<AuthResponse?>("Invalid credentials", 401, null);
        }

        var token = await GenerateToken(user);
        var payload = new AuthResponse { token = token };
        return new ApiResponse<AuthResponse?>("Login successful", 200, payload);
    }

    public async Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterDTO register)
    {
        var exists = await _userRepository.ExistsByEmailAsync(register.email);
        if (exists)
        {
            return new ApiResponse<AuthResponse>("User already exists", 409, null);
        }

        var user = new User
        {
            Email = register.email,
            IsActive = true,
            IsDeleted = false,
            IsBlocked = false,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow
        };

        var hasher = new PasswordHasher<User>();
        user.PasswordHash = hasher.HashPassword(user, register.password);

        await _userRepository.AddAsync(user);

        var token = await GenerateToken(user);
        var payload = new AuthResponse { token = token };
        return new ApiResponse<AuthResponse>("Registration successful", 201, payload);
    }

    public async Task<string> GenerateToken(User user)
    {
        // Support both "Jwt" and "JwtSettings" config sections
        var key = _configuration["Jwt:Key"] ?? _configuration["JwtSettings:Secret"];
        if (string.IsNullOrEmpty(key)) throw new InvalidOperationException("JWT key is not configured.");

        var issuer = _configuration["Jwt:Issuer"] ?? _configuration["JwtSettings:Issuer"] ?? "TrackFlow";
        var audience = _configuration["Jwt:Audience"] ?? _configuration["JwtSettings:Audience"] ?? "TrackFlowUsers";
        var expiryStr = _configuration["Jwt:ExpiryMinutes"] ?? _configuration["JwtSettings:ExpiryMinutes"];
        var expiryMinutes = 60;
        if (!string.IsNullOrEmpty(expiryStr) && int.TryParse(expiryStr, out var parsed)) expiryMinutes = parsed;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}