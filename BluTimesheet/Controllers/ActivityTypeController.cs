﻿using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using System.Web.Http;
using BluTimesheet.Models.DbModels;
using System.Threading.Tasks;

namespace BluTimesheet.Controllers
{
    //[Authorize(Roles = Startup.roleAdmin)]
    [Authorize]
    public class ActivityTypeController : ApiController
    {
        private IActivityTypeService activityTypeService;

        public ActivityTypeController(IActivityTypeService activityTypeService)
        {
            this.activityTypeService = activityTypeService;
        }


        public IHttpActionResult PostActivityType(ActivityType activityType)
        {
            activityTypeService.Add(activityType);
            return Ok();
        }

        
        public IEnumerable<ActivityType> GetActivityTypes()
        {
            return activityTypeService.GetAll();
        }



        public IHttpActionResult GetActivityType(int id)
        {
            var activity = activityTypeService.Get(id);
            if (activity != null)
            {
                return Ok(activity);
            }
            else
            {
                return NotFound();
            }

        }

        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult PutActivityType(ActivityType activityType)
        {
            var activityTypeFromDb = activityTypeService.Get(activityType.ActivityId);
            if (activityTypeFromDb != null)
            {
                activityTypeService.Update(activityType);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult DeleteActivityType(int id)
        {
            var activityTypeFromDb = activityTypeService.Get(id);
            if (activityTypeFromDb != null)
            {
                activityTypeService.Remove(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
