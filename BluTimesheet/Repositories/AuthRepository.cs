using BluTimesheet.Context;
using BluTimesheet.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BluTimesheet.Repositories
{
    public class AuthRepository : IDisposable
    {
        private TimesheetDbContext context;

        private UserManager<IdentityUser> userManager;

        public AuthRepository()
        {
            this.context = new TimesheetDbContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public async Task<IdentityResult> RegisterUser(User user)
        {
            IdentityUser identityUser = new IdentityUser
            {
                Email = user.Email
            };

            var result = await userManager.CreateAsync(identityUser, user.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userEmail, string password)
        {
            IdentityUser user = await userManager.FindAsync(userEmail, password);

            return user;
        }

        public void Dispose()
        {
            context.Dispose();
            userManager.Dispose();

        }
    }
}