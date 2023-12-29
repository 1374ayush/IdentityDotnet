using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityProject.Logic
{
    public class AccountLogic : IAccountLogic
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountLogic(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // signup process here...
        public async Task<IdentityResult> CreateUserAsync(User user) {

            //here we have created newUser which is of IdentityUser type becoz , our user manager is accepting IdentityUser type oject
            var newUser = new IdentityUser()
            {
                Email = user.Email,
                UserName = user.Email
            };

            // createAsync function accepts 2 parameter one is user detail and other is password.
            var result = await _userManager.CreateAsync(newUser, user.Password);

            return result;
        }

        // signin process here....

        public async Task<SignInResult> PasswordSignInAsync(SignInModel user)
        {
            var response = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

            return response;
        }

    }
}
