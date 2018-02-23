using BluTimesheet.Repositories;
using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using BluTimesheet.Models.DbModels;
using System;
using System.Threading.Tasks;
using System.Linq;
using BluTimesheet.Context;
using System.Data.Entity;
using System.Web;
using BluTimesheet.Authorization;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using BluTimesheet.EmailSender;

namespace BluTimesheet.Services.implementations
{
    public class ActivityService : IActivityService
    {
        private readonly ActivityTypeRepository activityTypeRepository;
        private readonly ProjectRoleTypeRepository projectRoleTypeRepository;
        private readonly ActivityRepository activityRepository;
        private readonly ProjectRepository projectRepository;
        private readonly TimesheetDbContext context;
        private ApplicationUserManager userManager;
        private readonly Email email;

        public ActivityService(TimesheetDbContext context, ActivityRepository activityRepository)
        {
            this.context = new TimesheetDbContext();
            this.activityTypeRepository = new ActivityTypeRepository(context);
            this.activityRepository = new ActivityRepository(context);
            this.projectRoleTypeRepository = new ProjectRoleTypeRepository(context);
            this.projectRepository = new ProjectRepository(context);
            this.email = new Email();
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public void Add(Activity dailyActivity)
        {
            var types = activityTypeRepository.GetAll();

            var exactType = types.First(x => x.ActivityId == dailyActivity.ActivityType.ActivityId);
            dailyActivity.ActivityType = exactType;

            var roles = projectRoleTypeRepository.GetAll();
            var roleType = roles.First(x => x.RoleId == dailyActivity.CurrentProjectRoleType.RoleId);
            dailyActivity.CurrentProjectRoleType = roleType;

            var projects = projectRepository.GetAll();
            var exactProject = projects.First(x => x.ProjectId == dailyActivity.Project.ProjectId);
            dailyActivity.Project = exactProject;

            activityRepository.Add(dailyActivity);
        }

        public void Approve(Activity dailyActivity)
        {
            activityRepository.Approve(dailyActivity);

        }

        public IEnumerable<Activity> GetAll()
        {
            return activityRepository.GetAll();
        }

        public Activity Get(int id)
        {
            return activityRepository.Get(id);
        }

        public void Remove(int id)
        {
            activityRepository.Remove(id);
        }

        public Activity Update(Activity dailyActivity)
        {
            Activity activityToEdit = context.Activity.Find(dailyActivity.Id);
            Project projectToEdit = context.Project.Find(dailyActivity.Project.ProjectId);
            ActivityType activityTypeToEdit = context.ActivityType.Find(dailyActivity.ActivityType.ActivityId);
            ProjectRoleType projectRoleToEdit = context.ProjectRoleType.Find(dailyActivity.CurrentProjectRoleType.RoleId);

            activityToEdit.Begining = dailyActivity.Begining;
            activityToEdit.HowManyHours = dailyActivity.HowManyHours;
            activityToEdit.description = dailyActivity.description;
            activityToEdit.ActivityType = activityTypeToEdit;
            activityToEdit.Project = projectToEdit;
            activityToEdit.CurrentProjectRoleType = projectRoleToEdit;
            activityToEdit.ApprovedByManager = dailyActivity.ApprovedByManager;

            context.Entry(activityToEdit).State = EntityState.Modified;
            context.SaveChanges();
            return activityToEdit;
        }

        public IEnumerable<Activity> GetActivitesByUser(string id)
        {
            return activityRepository.Search(x => x.UserId.Equals(id));
        }

        public IEnumerable<Activity> GetActivitesByProject(int id)
        {
            return activityRepository.Search(x => x.Project.ProjectId == id);
        }

        public IEnumerable<Activity> GetActivitesByActivityType(int id)
        {
            return activityRepository.Search(x => x.ActivityType.ActivityId == id);
        }

        public void SubmitToManager(Activity activity)
        {
            email.EmailSender(activity);

        }

        public IEnumerable<Activity> GetActivitesByProjectPerTime(DateTime TimeFrom, DateTime TimeTo, int id)
        {
            return activityRepository.Search(x => x.Begining >= TimeFrom && x.Begining <= TimeTo && x.Project.ProjectId == id);
        }

        public IEnumerable<Activity> GetActivitesByUserPerTime(DateTime TimeFrom, DateTime TimeTo, string id)
        {
            return activityRepository.Search(x => x.Begining >= TimeFrom && x.Begining <= TimeTo && x.UserId.Equals(id));
        }

        public IEnumerable<Activity> GetActivitesByActivityTypePerTime(DateTime TimeFrom, DateTime TimeTo, int id)
        {
            return null; // activityRepository.Search(x => x.Begining >= TimeFrom && x.End <= TimeTo && x.ActivityType.Id == id);
        }

        public IEnumerable<Activity> GetActivitesByApprovedByManagerPerTime(DateTime TimeFrom, DateTime TimeTo, bool id)
        {
            return null; // activityRepository.Search(x => x.Begining >= TimeFrom && x.End <= TimeTo && x.ApprovedByManager == id);
        }

        public IEnumerable<Activity> GetActivitesByProjectRoleTypePerTime(DateTime TimeFrom, DateTime TimeTo, int id)
        {
            return null; //activityRepository.Search(x => x.Begining >= TimeFrom && x.End <= TimeTo && x.CurrentProjectRoleType.RoleId == id);
        }


    }
}