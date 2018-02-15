using BluTimesheet.Context;
using BluTimesheet.Models.AuthenticationBindingModels;
using BluTimesheet.Repositories;
using BluTimesheet.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BluTimesheet.Services.implementations
{
    public class UserInfoService : IUserInfoService
    {
        private readonly UserInfoRepository userInfoRepository;


        public UserInfoService(TimesheetDbContext context)
        {
            this.userInfoRepository = new UserInfoRepository(context);
        }

        public UserInfoViewModel Get()
        {
            return userInfoRepository.GetUserInfo();
        }

        public IEnumerable<UserInfoViewModel> GetUsers()
        {
            return userInfoRepository.GetUsersInfo();
        }

        public IEnumerable<UserInfoViewModel> GetAllUsers()
        {
            return userInfoRepository.GetFullUsersInfo();
        }

        public UserInfoViewModel GetUsers(string id)
        {
            return userInfoRepository.GetUserInfo(id);
        }
    }
}