namespace BluTimesheet.Migrations
{
    using BluTimesheet.Authorization;
    using BluTimesheet.Context;
    using BluTimesheet.Models.DbModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Data.Entity.Migrations;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<TimesheetDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TimesheetDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(Startup.roleAdmin))
            {
                roleManager.Create(new IdentityRole(Startup.roleAdmin));
            }

            if (!roleManager.RoleExists(Startup.roleUser))
            {
                roleManager.Create(new IdentityRole(Startup.roleUser));
            }

            if (!roleManager.RoleExists(Startup.roleManager))
            {
                roleManager.Create(new IdentityRole(Startup.roleManager));
            }


            var userKrzysztof = userManager.FindByEmail("k.kluszczynski@blunovation.com");

            if (userKrzysztof == null)
            {
                userKrzysztof = new ApplicationUser
                {
                    Email = "k.kluszczynski@blunovation.com",
                    UserName = "k.kluszczynski@blunovation.com",
                    FirstName = "Krzysztof",
                    LastName = "Kluszczynski",

                };
                string passwordKrzysztof = "Password1!";
                userManager.Create(userKrzysztof, passwordKrzysztof);
                userManager.AddToRole(userKrzysztof.Id, Startup.roleAdmin);
            }
            
            var userPiotr = userManager.FindByEmail("p.nasinski@blunovation.com");
            
            if (userPiotr == null)
            {
                userPiotr = new ApplicationUser
                {
                    Email = "p.nasinski@blunovation.com",
                    UserName = "p.nasinski@blunovation.com",
                    FirstName = "Piotr",
                    LastName = "Nasiñski",
                    SuperiorId = userKrzysztof.Id

                };
                string passwordPiotr = "Password1!";
                userManager.Create(userPiotr, passwordPiotr);
                userManager.AddToRole(userPiotr.Id, Startup.roleManager);
            }

            var userJakub = userManager.FindByEmail("j.wycinka@blunovation.com");

            if (userJakub == null)
            {
                userJakub = new ApplicationUser
                {
                    Email = "j.wycinka@blunovation.com",
                    UserName = "j.wycinka@blunovation.com",
                    FirstName = "Jakub",
                    LastName = "Wycinka",
                    SuperiorId = userPiotr.Id

                };
                string passwordJakub = "Password1!";
                userManager.Create(userJakub, passwordJakub);
                userManager.AddToRole(userJakub.Id, Startup.roleUser);
            }

            var userSlawomir = userManager.FindByEmail("s.rzeppa@blunovation.com");

            if (userSlawomir == null)
            {
                userSlawomir = new ApplicationUser
                {
                    Email = "s.rzeppa@blunovation.com",
                    UserName = "s.rzeppa@blunovation.com",
                    FirstName = "S³awomir",
                    LastName = "Rzeppa",
                    SuperiorId = userPiotr.Id

                };
                string passwordSlawomir = "Password1!";
                userManager.Create(userSlawomir, passwordSlawomir);
                userManager.AddToRole(userSlawomir.Id, Startup.roleUser);
            }


            ProjectType projectBillable = new ProjectType { Name = "Billable" };
            ProjectType projectNonBillable = new ProjectType { Name = "Non-Billable" };

            Project project1 = new Project { Name = "Timesheet", ProjectType = projectNonBillable, Client= "XXX", Projectnumber = "B21" };
            Project project2 = new Project { Name = "Artifex", ProjectType = projectBillable, Client= "VVV", Projectnumber="B22" };
            Project project3 = new Project { Name = "DD-PACK", ProjectType = projectBillable, Client = "YYY", Projectnumber = "C32" };
            Project project4 = new Project { Name = "IMPAQ", ProjectType = projectBillable, Client="XXX", Projectnumber = "D21" };


            ActivityType activityType1 = new ActivityType { Name = "Working" };
            ActivityType activityType2 = new ActivityType { Name = "Sickness" };
            ActivityType activityType3 = new ActivityType { Name = "Holidays" };
            ActivityType activityType4 = new ActivityType { Name = "Bank Holidays" };
            ActivityType activityType5 = new ActivityType { Name = "Event" };
            ActivityType activityType6 = new ActivityType { Name = "Other" };

            ProjectRoleType projectRoleType1 = new ProjectRoleType { RoleName = "Back-End Developer" };
            ProjectRoleType projectRoleType2 = new ProjectRoleType { RoleName = "Front-end Developer" };
            ProjectRoleType projectRoleType3 = new ProjectRoleType { RoleName = "Team leader" };
            ProjectRoleType projectRoleType4 = new ProjectRoleType { RoleName = "Other" };

            Activity activity1 = new Activity
            {
                Begining = DateTime.Now,
                Project = project1,
                CurrentProjectRoleType = projectRoleType2,
                ActivityType = activityType1,
                UserId = userJakub.Id,
                HowManyHours = 8,
            };
        Activity activity2 = new Activity
        {
            Begining = DateTime.Now.AddDays(-1),
          //  End = DateTime.Now.AddDays(-1).AddHours(8),
            Project = project1,
            CurrentProjectRoleType = projectRoleType2,
            ActivityType = activityType1,
            UserId = userJakub.Id,
            HowManyHours = 8,
            };
            Activity activity3 = new Activity
            {
                Begining = DateTime.Now.AddDays(-2),
               // End = DateTime.Now.AddDays(-2).AddHours(8),
                Project = project1,
                CurrentProjectRoleType = projectRoleType1,
                ActivityType = activityType1,
                UserId = userJakub.Id,
                HowManyHours = 7,
            };
            Activity activity4 = new Activity
            {
                Begining = DateTime.Now.AddDays(-3),
                //End = DateTime.Now.AddDays(-3).AddHours(8),
                CurrentProjectRoleType = projectRoleType4,
                Project = project1,
                ActivityType = activityType1,
                UserId = userJakub.Id,
                HowManyHours = 8,
            };

            context.ProjectType.AddOrUpdate(x => x.Name, projectBillable, projectNonBillable);
            context.Project.AddOrUpdate(x => x.Name, project1, project2, project3, project4);
            context.ActivityType.AddOrUpdate(x => x.Name, activityType1, activityType2,
                activityType3, activityType4, activityType5, activityType6);
            context.ProjectRoleType.AddOrUpdate(x => x.RoleName, projectRoleType1, projectRoleType2,
                projectRoleType3, projectRoleType4);
            context.Activity.AddOrUpdate(x => x.Id, activity1, activity2, activity3, activity4);
           // context.Users.AddOrUpdate(x => x.Id, userJakub, userKrzysztof, userPiotr, userSlawomir);
            context.SaveChanges();
        }
    }
}
