using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace TrackFlow.Application.Helper;

public class JWTHelper
{
    public static  void ConfigureJwt(IServiceCollection services, IConfiguration config, IHostEnvironment env)
    {
        var jwtKey = config["Jwt:Key"] 
                     ?? config["JwtSettings:Key"] 
                     ?? config["JwtSettings:Secret"];

        if (string.IsNullOrEmpty(jwtKey))
        {
            if (env.IsDevelopment())
            {
                jwtKey = "TrackFlow_Dev_Key_CHANGE_ME_IN_PROD_!@#";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Using fallback JWT key for development. Update your appsettings for production!");
                Console.ResetColor();
            }
            else
            {
                throw new InvalidOperationException("JWT key is not configured. Please set Jwt:Key or JwtSettings:Secret in configuration.");
            }
        }

        // ⚠️ Ensure minimum 32 characters (256 bits) for HS256
        if (Encoding.UTF8.GetByteCount(jwtKey) < 32)
            throw new ArgumentException("JWT key must be at least 32 characters long for HS256 security.");

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"] ?? config["JwtSettings:Issuer"] ?? "TrackFlow",
            ValidAudience = config["Jwt:Audience"] ?? config["JwtSettings:Audience"] ?? "TrackFlowUsers",
            IssuerSigningKey = signingKey
        };

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
    }

}