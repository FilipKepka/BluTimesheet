using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BluTimesheet.Models.DbModels
{
    public class Project
    {
        //public Project()
       // {
          //  this.Activity = new HashSet<Activity>();
       // }
        [Key]
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Projectnumber { get; set; }

        public virtual ProjectType ProjectType { get; set; }

        //  
       // public virtual ICollection<Activity> Activity { get; set; }
    }
}