using Microsoft.AspNetCore.Mvc;

namespace TrackFlow.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    [HttpPost("signup")]
    public IActionResult Post()
    {
        return View();
    }
}