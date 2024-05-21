using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult CheckAuthorize()
        {
            return Ok("I will only run when u r loged in");
        }

        [Authorize(Roles ="admin")]
        [HttpGet("admin")]
        public IActionResult CheckAuthorizeAdmin()
        {
            return Ok("I will only run when u r loged in through admin");
        }
    }
}
