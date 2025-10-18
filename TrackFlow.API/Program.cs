using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TrackFlow.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);


// Read configuration with support for both "Jwt" and "JwtSettings" sections
var _jwtKey = builder.Configuration["Jwt:Key"] ?? builder.Configuration["JwtSettings:Key"] ?? builder.Configuration["JwtSettings:Secret"];
if (string.IsNullOrEmpty(_jwtKey))
{
    if (builder.Environment.IsDevelopment())
    {
        _jwtKey = "TrackFlow_Dev_Key_CHANGE_ME_IN_PROD_!@#";
        Console.WriteLine("Warning: JWT key not found in configuration. Using development fallback key.");
    }
    else
    {
        throw new InvalidOperationException("JWT key is not configured. Please set Jwt:Key or JwtSettings:Secret in configuration.");
    }
}

var _issuer = builder.Configuration["Jwt:Issuer"] ?? builder.Configuration["JwtSettings:Issuer"] ?? "TrackFlow";
var _audience = builder.Configuration["Jwt:Audience"] ?? builder.Configuration["JwtSettings:Audience"] ?? "TrackFlowUsers";

var _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
var _tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = _issuer,
    ValidAudience = _audience,
    IssuerSigningKey = _signingKey
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { options.TokenValidationParameters = _tokenValidationParameters; });
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackFlow API v1");
        options.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();