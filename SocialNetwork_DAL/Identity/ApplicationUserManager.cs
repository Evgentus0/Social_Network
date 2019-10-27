using Microsoft.AspNet.Identity;
using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Identity
{
    public class ApplicationUserManager:UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
        }
    }   
}
