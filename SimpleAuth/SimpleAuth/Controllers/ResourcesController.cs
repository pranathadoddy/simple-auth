using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimpleAuth.Controllers
{

    [ApiController]
    public class ResourcesController : ControllerBase
    {
        [HttpGet("api/resources")]
        [Authorize(Roles = "User")]
        public IActionResult GetResources()
        {
            return Ok($"protected resources, username: {User.Identity!.Name}");
        }
    }
}
