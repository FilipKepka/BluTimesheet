using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BluTimesheet.Models.AuthenticationBindingModels
{
    public class UserEditModel
    {
        public string userId { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string superiorId { get; set; }

        public IList<string> role { get; set; }
    }
}