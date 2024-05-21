using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityProject.Logic
{
    public class AccountLogic : IAccountLogic
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountLogic(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration= configuration;
        }


        // signup process here...
        public async Task<IdentityResult> CreateUserAsync(User user, string role) {

            //here we have created newUser which is of IdentityUser type becoz , our user manager is accepting IdentityUser type oject
            var newUser = new IdentityUser()
            {
                Email = user.Email,
                UserName = user.Email
            };

            // createAsync function accepts 2 parameter one is user detail and other is password.
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(newUser, role);
                if (!roleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(newUser);
                    return roleResult;
                }
            }

            return result;
        }

        // signin process here....

        public async Task<UserManagerResponseViewModel> PasswordSignInAsync(SignInModel user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

            if (result.Succeeded)
            {
                //generate token
                var loginUser = await _userManager.FindByEmailAsync(user.Email);
                if (loginUser != null)
                {
                    string tempRole = "";
                    var roles = await _userManager.GetRolesAsync(loginUser);
                    if(roles.Count() > 0) { tempRole = roles[0]; }
                    string token = GenerateToken(user, tempRole);

                    return new UserManagerResponseViewModel
                    {
                        Message = token,
                        IsSuccess = true,
                    };
                }

            }
            return new UserManagerResponseViewModel
            {
                Message = "Invalid credential",
                IsSuccess = false,

            };
        }

        private string GenerateToken(SignInModel userObj, string role)
        {
            //create claims
            var claims = new[]
            {
                new Claim("Email",userObj.Email),
                new Claim(ClaimTypes.Role,role)
            };

            //get the key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenAsString;
        }

    }
}
