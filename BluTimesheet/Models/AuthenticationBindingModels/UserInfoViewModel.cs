using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BluTimesheet.Models.AuthenticationBindingModels
{
    public class UserInfoViewModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string userId { get; set; }

        public bool IsAdmin { get; set; }
    }
}