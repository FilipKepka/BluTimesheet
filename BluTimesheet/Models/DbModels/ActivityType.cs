using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BluTimesheet.Models.DbModels
{
    public class ActivityType
    {
       // public ActivityType()
      //  {
            //this.Activity = new HashSet<Activity>();
      //  }
        [Key]
        public int ActivityId { get; set; }

        public string Name { get; set; }

    }
}