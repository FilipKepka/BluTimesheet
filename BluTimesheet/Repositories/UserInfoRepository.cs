using BluTimesheet.Context;
using BluTimesheet.Models.AuthenticationBindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BluTimesheet.Repositories
{
    public class UserInfoRepository : GenericRepository<UserInfoViewModel>
    {
        public UserInfoRepository(TimesheetDbContext context) : base(context)
        {

        }
    }
}