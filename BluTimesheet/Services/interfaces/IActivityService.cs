using BluTimesheet.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluTimesheet.Services.interfaces
{
    public interface IActivityService
    {
        void Add(Activity activity);
        void Remove(int id);
        Activity Get(int id);
        void Update(Activity activity);        
        void Approve(Activity activity);
        IEnumerable<Activity> GetAll(); 
        IEnumerable<Activity> GetActivitesByUser(string id);
        IEnumerable<Activity> GetActivitesByProject(int id);
        IEnumerable<Activity> GetActivitesByActivityType(int id);
        void SubmitToManager(int id);
        IEnumerable<Activity> GetActivitesByProjectPerTime(DateTime TimeFrom, DateTime TimeTo, int id);
        IEnumerable<Activity> GetActivitesByUserPerTime(DateTime TimeFrom, DateTime TimeTo, string id);
        IEnumerable<Activity> GetActivitesByActivityTypePerTime(DateTime TimeFrom, DateTime TimeTo, int id);
        IEnumerable<Activity> GetActivitesByApprovedByManagerPerTime(DateTime TimeFrom, DateTime TimeTo, bool id);
        IEnumerable<Activity> GetActivitesByProjectRoleTypePerTime(DateTime TimeFrom, DateTime TimeTo, int id);
       // IEnumerable<Activity> GetActivitesByProjectRoleTypePerTime(DateTime TimeFrom, DateTime TimeTo, int id);
    }
}
