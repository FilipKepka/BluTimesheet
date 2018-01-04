using BluTimesheet.Repositories;
using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using BluTimesheet.Models.DbModels;
using System.Threading.Tasks;

namespace BluTimesheet.Services.implementations
{
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly ActivityTypeRepository activityTypeRepository;

        public ActivityTypeService(ActivityTypeRepository activityTypeRepository)
        {
            this.activityTypeRepository = activityTypeRepository;
        }

        public void Add(ActivityType activityType)
        {
            activityTypeRepository.Add(activityType);
        }

        public IEnumerable<ActivityType> GetAll()
        {
            return activityTypeRepository.GetAll();
        }

        public ActivityType Get(int id)
        {
            return activityTypeRepository.Get(id);
        }

        public void Remove(int id)
        {
            activityTypeRepository.Remove(id);
        }

        public void Update(ActivityType activityType)
        {
            activityTypeRepository.Update(activityType);
        }
        
    }
}