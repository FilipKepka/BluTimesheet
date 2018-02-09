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
            userInfoRepository = new UserInfoRepository(context);
        }

        public UserInfoViewModel Get(string id)
        {
            return userInfoRepository.Get(id);
        }
    }
}