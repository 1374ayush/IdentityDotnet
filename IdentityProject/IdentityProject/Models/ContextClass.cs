using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Models
{
    public class ContextClass:IdentityDbContext
    {
        public ContextClass(DbContextOptions<ContextClass> options):base(options) { }   
    }
}
