using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TrackFlow.Application.Helper;
using TrackFlow.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// 🔧 Configure Services
// -----------------------------
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -----------------------------
// 🔐 Configure JWT
// -----------------------------
JWTHelper.ConfigureJwt(builder.Services, builder.Configuration, builder.Environment);

// -----------------------------
// 🚀 Build App
// -----------------------------
var app = builder.Build();

// -----------------------------
// 🧭 Middleware Pipeline
// -----------------------------
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();


