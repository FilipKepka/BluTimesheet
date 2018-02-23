
using BluTimesheet.Authorization;
using BluTimesheet.Context;
using BluTimesheet.Models.AuthenticationBidingModels;
using BluTimesheet.Models.AuthenticationBindingModels;
using BluTimesheet.Services.implementations;
using BluTimesheet.Services.interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using System.Data.Entity;
using System.Linq;
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
        readonly IUserInfoService userInfoService;


        public AccountController(IUserInfoService userInfoService)
        {
            this.userInfoService = userInfoService;
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
        [Route("ChangePasswordWithoutOld")]
        [Authorize]
        public async Task<IHttpActionResult> ChangePasswordWithoutOld(ChangePasswordModel model)
        {
            if (User.IsInRole(Startup.roleAdmin)) {
                var userData = userManager.FindById(model.user);
                var resetCode = userManager.GeneratePasswordResetToken(model.user);
                var result = userManager.ResetPassword(model.user, resetCode, model.NewPassword);
                return Ok(result);
            }
            return Unauthorized();
        }


        [Route("ChangePassword")]
        [Authorize]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
                var result = await userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                return Ok(result);
        }


        // POST api/Account/SetPassword
        [Route("SetPassword")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordModel model)
        {
            var result = await userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
            
            return Ok(result);
        }


        // POST api/Account/Register
        [Authorize(Roles = "Admin, Manager")]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email, LastName = model.lastName, FirstName = model.firstName,
            SuperiorId = model.superiorId };

            var result = await userManager.CreateAsync(user, model.Password);
            var userInfo = userManager.FindByEmail(user.Email);
            if (model.role[0] == "Admin")
            {
                userManager.AddToRole(userInfo.Id, Startup.roleAdmin);
            }
            else if (model.role[0] == "Manager")
            {
                userManager.AddToRole(userInfo.Id, Startup.roleManager);
            }
            else
            {
                userManager.AddToRole(userInfo.Id, Startup.roleUser);
            }


            return Ok(result);
        }


        // POST api/Account/UserEditU
        [Authorize(Roles = "Admin, Manager")]
        [Route("UserEdit")]
        public async Task<IHttpActionResult> UserEdit(UserEditModel model)
        {
            var userToEdit = userManager.FindById(model.userId);
            userToEdit.Email = model.Email;
            userToEdit.FirstName = model.firstName;
            userToEdit.LastName = model.lastName;
            userToEdit.SuperiorId = model.superiorId;
            userToEdit.UserName = model.Email;



            if (userManager.GetRoles(model.userId).Contains(model.role[0]))
            { }
            else
            {
                userToEdit.Roles.Clear();
                if (model.role[0] == "Admin")
                {
                    userManager.AddToRole(model.userId, Startup.roleAdmin);
                }
                else if (model.role[0] == "Manager")
                {
                    userManager.AddToRole(model.userId, Startup.roleManager);
                }
                else
                {
                    userManager.AddToRole(model.userId, Startup.roleUser);
                }
            }
            userManager.Update(userToEdit);
            return Ok();
        }

        //POST: /Account/RemoveUser/{Id}
        //  [AllowAnonymous]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("RemoveUser/{Id}")]
        public async Task<IHttpActionResult> GetRemoveUser(string Id)
        {
            var userToRemove = userManager.FindById(Id);
            userManager.Delete(userToRemove);
            return Ok();
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
