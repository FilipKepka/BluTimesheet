using BluTimesheet.Models.DbModels;
using System.Collections.Generic;
namespace BluTimesheet.Services.interfaces
{
    public interface IActivityTypeService
    {
        void Add(ActivityType activityType);
        void Remove(int id);
        ActivityType Get(int id);
        void Update(ActivityType activityType);
        IEnumerable<ActivityType> GetAll();
    }
}
