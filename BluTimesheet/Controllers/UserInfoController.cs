using BluTimesheet.Services.interfaces;
using System.Web.Http;
using System.Collections.Generic;
using BluTimesheet.Models.DbModels;
using BluTimesheet.Authorization;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using BluTimesheet.Repositories;
using BluTimesheet.Services.implementations;

namespace BluTimesheet.Controllers
{

    [RoutePrefix("api/userInfo")]
    public class UserInfoController : ApiController
    {
        private ApplicationUserManager userManager;
        IUserInfoService userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            this.userInfoService = userInfoService;
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        [Route("MyUserInfo")]
        public IHttpActionResult GetUserInfo()
        {
            return Ok(userInfoService.Get());
        }

        [Route("UserInfo")]
        public IHttpActionResult GetUsersInfo()
        {
            return Ok(userInfoService.GetUsers());
        }

        [Route("AllUserInfo")]
        public IHttpActionResult GetAllUsersInfo()
        {
            return Ok(userInfoService.GetAllUsers());
        }

        [Route("SingleUserInfo/{id}")]
        public IHttpActionResult GetUserInfo(string id)
        {
            return Ok(userInfoService.GetUsers(id));
        }
    }
}