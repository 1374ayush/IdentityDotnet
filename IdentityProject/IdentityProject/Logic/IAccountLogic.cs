using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityProject.Logic
{
    public interface IAccountLogic
    {
        Task<IdentityResult> CreateUserAsync(User user);
    }
}