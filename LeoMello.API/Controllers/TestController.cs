using LeoMello.Application.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeoMello.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/")]
    public class TestController : ControllerBase
    {
        [HttpPost("/test")]
        public async Task<IActionResult> TestAsync(string something)
        {
            return Ok();
        }
    }
}
