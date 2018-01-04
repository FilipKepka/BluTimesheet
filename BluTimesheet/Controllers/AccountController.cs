
using BluTimesheet.Authorization;
using BluTimesheet.Models.AuthenticationBidingModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BluTimesheet.Controllers
{
    
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager userManager;
        private ApplicationSignInManager signInManager;
        private ApplicationRoleManager roleManager;

        public AccountController()
        {
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            signInManager = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
        }
        
        
        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }
               

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
            var result = await userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,model.NewPassword);

            return Ok(result);
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordModel model)
        {
            var result = await userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
            
            return Ok(result);
        }
                
        
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            var result = await userManager.CreateAsync(user, model.Password);

            return Ok(result);
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginModel model)
        {
            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, shouldLockout: false);

            return Ok(result);
        }        
    }
}
