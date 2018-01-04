using BluTimesheet.Authorization;
using BluTimesheet.Models.DbModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BluTimesheet.Context
{
    public class TimesheetDbContext : IdentityDbContext<ApplicationUser>
    {
        public TimesheetDbContext() : base("BluTimesheet")
        {
        }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<ActivityType> ActivityType { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectType> ProjectType { get; set; }
        public DbSet<ProjectRoleType> ProjectRoleType { get; set; }

        public static TimesheetDbContext Create()
        {
            return new TimesheetDbContext();
        }
    }
}