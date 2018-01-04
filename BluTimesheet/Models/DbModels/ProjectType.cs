using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BluTimesheet.Models.DbModels
{
    public class ProjectType
    {
      //  public ProjectType()
      //  {
       //     this.Project = new HashSet<Project>();
      //  }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        //
    //    public virtual ICollection<Project> Project { get; set; }

    }
}