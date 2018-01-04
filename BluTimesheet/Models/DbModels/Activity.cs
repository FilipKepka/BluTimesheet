using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BluTimesheet.Models.DbModels
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime? Begining { get; set; }

        public DateTime? End { get; set; }

        public string description { get; set; }

        [Required]
        public ActivityType ActivityType { get; set; }//

        public ProjectRoleType ProjectRoleType { get; set; }//

        public Project Project { get; set; }//virtual

        public bool ApprovedByManager { get; set; }

        [Required]
        public string UserId { get; set; }
        



    }
}