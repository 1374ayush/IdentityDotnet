using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityProject.Logic
{
    public class AccountLogic : IAccountLogic
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountLogic(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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

    }
}
