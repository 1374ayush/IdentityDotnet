using IdentityProject.Logic;
using IdentityProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountLogic _accountLogic;

        public AccountController(IAccountLogic accountLogic)
        {
            _accountLogic = accountLogic;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            var response =await _accountLogic.CreateUserAsync(user);

            if (response.Succeeded)
            {
                return Ok(true);
            }else
            { 
                return BadRequest(response.Errors);
            }
            
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> SignIn(SignInModel user)
        {
            var response =  await _accountLogic.PasswordSignInAsync(user);

            if (response.Succeeded)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest("error");
            }
        }

    }
}
