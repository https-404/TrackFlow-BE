using Microsoft.AspNetCore.Mvc;

namespace TrackFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "Healthy",
            timestamp = DateTime.UtcNow
        });
    } 
}
