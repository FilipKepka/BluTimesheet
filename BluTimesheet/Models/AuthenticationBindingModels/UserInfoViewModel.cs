using BluTimesheet.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BluTimesheet.Models.AuthenticationBindingModels
{
    public class UserInfoViewModel
    {
        public string FistName { get; set; }

        public string LastName { get; set; }

        public string userId { get; set; }

        public bool IsAdmin { get; set; }

        public string SuperiorId { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string UserName { get; set; }

        public IList<string> role { get; set; }
    }
}