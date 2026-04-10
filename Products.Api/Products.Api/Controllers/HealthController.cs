using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("api/Health")]
    public class HealthController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok("Ok");
        }
    }
}
