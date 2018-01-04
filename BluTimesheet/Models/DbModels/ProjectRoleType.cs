using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BluTimesheet.Models.DbModels
{
    public class ProjectRoleType
    {
       // public ProjectRoleType()
       // {
       //     this.Activity = new HashSet<Activity>();
      //  }
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }


        //
      //  public virtual ICollection<Activity> Activity { get; set; }
    }
}