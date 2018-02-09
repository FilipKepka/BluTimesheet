using BluTimesheet.Models.AuthenticationBindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluTimesheet.Services.interfaces
{
    public interface IUserInfoService
    {
        UserInfoViewModel Get(string id);
    }
}
