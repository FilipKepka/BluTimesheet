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
      //z  }
       [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<Activity> Activityes { get; set; }
        //
      //  public virtual ICollection<Activity> Activity { get; set; }
    }
}