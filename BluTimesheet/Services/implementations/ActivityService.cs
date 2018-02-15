using BluTimesheet.Repositories;
using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using BluTimesheet.Models.DbModels;
using System;
using System.Threading.Tasks;
using System.Linq;
using BluTimesheet.Context;

namespace BluTimesheet.Services.implementations
{
    public class ActivityService : IActivityService
    {
        private readonly ActivityTypeRepository activityTypeRepository;

        private readonly ProjectRoleTypeRepository projectRoleTypeRepository;

        private readonly ActivityRepository activityRepository;

        private readonly ProjectRepository projectRepository;

        public ActivityService(TimesheetDbContext context, ActivityRepository activityRepository)
        {
            this.activityTypeRepository = new ActivityTypeRepository(context);
            this.activityRepository = new ActivityRepository(context);
            this.projectRoleTypeRepository = new ProjectRoleTypeRepository(context);
            this.projectRepository = new ProjectRepository(context);
        }

        public void Add(Activity dailyActivity)
        {
            /*  var types = activityTypeRepository.GetAll();
              var exactType = types.First(x => x.Name == dailyActivity.ActivityType.Name);

              dailyActivity.ActivityType = exactType;
              activityRepository.Add(dailyActivity);*/

            var types = activityTypeRepository.GetAll();
            // var exactType = types.First(x => x.Name == dailyActivity.ActivityType.Name);
            var exactType = types.First(x => x.ActivityId == dailyActivity.ActivityType.ActivityId);
            dailyActivity.ActivityType = exactType;

            var roles = projectRoleTypeRepository.GetAll();
            var roleType = roles.First(x => x.RoleId == dailyActivity.CurrentProjectRoleType.RoleId);
            dailyActivity.CurrentProjectRoleType = roleType;

            var projects = projectRepository.GetAll();
            var exactProject = projects.First(x => x.ProjectId == dailyActivity.Project.ProjectId);
            dailyActivity.Project = exactProject;

            //dailyActivity.ActivityType.Id = exactType.Id;
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

        public void Update(Activity dailyActivity)
        {
            activityRepository.Update(dailyActivity);
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

        public void SubmitToManager(int id)
        {
            throw new System.NotImplementedException();
            //TODO
            //send email
        }


        //
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