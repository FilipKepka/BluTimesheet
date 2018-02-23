using BluTimesheet.Authorization;
using BluTimesheet.Context;
using BluTimesheet.Models.AuthenticationBindingModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Web.Security;

namespace BluTimesheet.Repositories
{
    public class UserInfoRepository : ApiController
    {
        private ApplicationUserManager userManager;
        protected TimesheetDbContext context;
        protected DbSet<UserInfoViewModel> dbSet;

        public UserInfoRepository(TimesheetDbContext context)
        {
            this.context = context;
            dbSet = context.Set<UserInfoViewModel>();
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public UserInfoViewModel GetUserInfo()
        {
            var userEmail = User.Identity.GetUserName();
            var userInfo = userManager.FindByEmail(userEmail);

            return new UserInfoViewModel
            {

                userId = User.Identity.GetUserId(),
                IsAdmin = true,
                FistName = userInfo.FirstName,
                LastName = userInfo.LastName,
                role = userManager.GetRoles(userInfo.Id),
            };
        }

        public IEnumerable<UserInfoViewModel> GetUsersInfo()
        {
            List<UserInfoViewModel> userInfoList = new List<UserInfoViewModel>();
            var context = new TimesheetDbContext();
            var allUsers = context.Users.ToList();
            foreach (var user in allUsers)
            {
                userInfoList.Add(
                    new UserInfoViewModel
                    {
                        userId = user.Id,
                        IsAdmin = true,
                        FistName = user.FirstName,
                        LastName = user.LastName,
                    });
            }
            return userInfoList;
        }


        public IEnumerable<UserInfoViewModel> GetFullUsersInfo()
        {
            List<UserInfoViewModel> userInfoList = new List<UserInfoViewModel>();
            var context = new TimesheetDbContext();
            var allUsers = context.Users.ToList();

            foreach (var user in allUsers)
            {

                string UserName2 = user.Id;
                var roles = userManager.GetRoles(UserName2);
               // string oneRole = roles[0];
                userInfoList.Add(
                    new UserInfoViewModel
                    {
                        userId = user.Id,
                        IsAdmin = true,
                        FistName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PasswordHash = user.PasswordHash,
                        SecurityStamp = user.SecurityStamp,
                        SuperiorId = user.SuperiorId,
                        UserName = user.UserName,
                        role = roles,

                    });
            }
            return userInfoList;
        }


        public UserInfoViewModel GetUserInfo(string id)
        {
            var User = userManager.FindById(id);

            return new UserInfoViewModel
            {
                userId = User.Id,
                IsAdmin = true,
                FistName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                SuperiorId = User.SuperiorId,
                role = userManager.GetRoles(id),
            } ;
        }
    }
}