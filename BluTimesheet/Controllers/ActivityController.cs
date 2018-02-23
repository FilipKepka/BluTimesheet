using BluTimesheet.Models.DbModels;
using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using BluTimesheet.Authorization;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Net;

namespace BluTimesheet.Controllers
{
    [Authorize]
    public class ActivityController : ApiController
    {
        private IActivityService activityService;
        private ApplicationUserManager userManager;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
            this.userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }


        public IHttpActionResult PostActivity(Activity activity)
        {
            activity.UserId = User.Identity.GetUserId();
            activityService.Add(activity);
            return Ok();
        }

        //[Authorize(Roles = Startup.roleAdmin)]
        public IEnumerable<Activity> GetActivities()
        {
            return activityService.GetAll();
        }

        [Route("api/user/{id}/activities")]
        [Route("api/user/activities")]
        public IHttpActionResult GetActivitiesByUser(string id = "")
        {
            bool insertedId = id != "";

            if ((User.IsInRole(Startup.roleAdmin) && insertedId) || (User.IsInRole(Startup.roleManager) && insertedId))
            {
                return Ok(activityService.GetActivitesByUser(id));
            }
            else if (User.IsInRole(Startup.roleManager) && insertedId)
            {
                var user = userManager.FindById(id);

                if(user == null)
                {
                    return NotFound();
                }
                else if (user.SuperiorId == User.Identity.GetUserId())
                {
                    return Ok(activityService.GetActivitesByUser(id));
                } else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Ok(activityService.GetActivitesByUser(User.Identity.GetUserId()));
            }
            
        }

        //[Authorize(Roles = Startup.roleAdmin)]
        [Route("api/project/{id}/activities")]
        public IEnumerable<Activity> GetActivitiesByProject(int id)
        {
            return activityService.GetActivitesByProject(id);
        }

        //[Authorize(Roles = Startup.roleAdmin)]
        [Route("api/activitytype/{id}/activities")]
        public IEnumerable<Activity> GetActivitiesByActivityType(int id)
        {
            return activityService.GetActivitesByActivityType(id);
        }

        public IHttpActionResult GetActivity(int id)
        {
            var activity = activityService.Get(id);
            if (!(activity == null))
            {
                bool isAuthor = activity.UserId == User.Identity.GetUserId();

                if (activity != null && isAuthor || User.IsInRole(Startup.roleAdmin) || User.IsInRole(Startup.roleManager))
                {
                    return Ok(activity);
                }
                else if (activity != null && !isAuthor)
                {
                    return Unauthorized();
                }
                else
                {
                    return NotFound();
                }
            }
            else{
                throw new HttpException((int)HttpStatusCode.NotFound, string.Format("No activity found"));
            }
        }

        public IHttpActionResult PutActivity(Activity activity)
        {
            var activityFromDb = activityService.Get(activity.Id);
            bool isAuthor = activityFromDb.UserId == User.Identity.GetUserId();

            if ((activityFromDb != null && isAuthor && activityFromDb.ApprovedByManager == false) || 
                activityFromDb !=null && User.IsInRole(Startup.roleAdmin) || User.IsInRole(Startup.roleManager) )
            {
                var returnData = activityService.Update(activity);
                return Ok(returnData);
            }
            else if ((activityFromDb != null && !isAuthor) || (activityFromDb != null && isAuthor && activityFromDb.ApprovedByManager == true))
            {
                return Unauthorized();
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult DeleteActivity(int id)
        {
            var activity = activityService.Get(id);
            bool isAuthor = activity.UserId == User.Identity.GetUserId();

            if ((activity != null && isAuthor) || 
                (activity !=null && (User.IsInRole(Startup.roleAdmin) || User.IsInRole(Startup.roleManager)))) // || admin
            {
                activityService.Remove(id);
                return Ok();
            }
            else if (activity != null && !isAuthor)
            {
                return Unauthorized();
            }
            else
            {
                return NotFound();
            }            
        }

        public IHttpActionResult SubmitActivityToManager(string id)
        {
            /* var activityFromDb = activityService.Get(id);
             bool isAuthor = activityFromDb.UserId == User.Identity.GetUserId();

             if (activityFromDb != null && isAuthor && activityFromDb.ApprovedByManager == false)
             {
                 activityService.SubmitToManager(id);
                 return Ok();
             }
             else if (activityFromDb != null && !isAuthor)
             {
                 return Unauthorized();
             }
             else
             {
                 return NotFound();
             } */
            return NotFound();
        }

        [HttpPost]
        [Route("api/sendEmailToSuperior")]
        public IHttpActionResult sendEmailToSuperior(Activity activity)
        {
            activityService.SubmitToManager(activity);
            return Ok();
        }

        //[Authorize(Roles = Startup.roleAdmin)]
        [Route("api/activitiesbyproject/{TimeFrom}/{TimeTo}/{Id}")]
        public IHttpActionResult GetActivitesByProjectPerTime(DateTime TimeFrom, DateTime TimeTo, int id)
        {
           return Ok(activityService.GetActivitesByProjectPerTime(TimeFrom, TimeTo, id));
        }

        //[Authorize(Roles = Startup.roleAdmin)]
        [Route("api/activitiesbyuser/{TimeFrom}/{TimeTo}/{Id}")]
        public IHttpActionResult GetActivitesByUserPerTime(DateTime TimeFrom, DateTime TimeTo, string id)
        {
            return Ok(activityService.GetActivitesByUserPerTime(TimeFrom, TimeTo, id));
        }

        //[Authorize(Roles = Startup.roleAdmin)]
        [Route("api/activitiesbyactivitytype/{TimeFrom}/{TimeTo}/{Id}")]
        public IHttpActionResult GetActivitesByActivityTypePerTime(DateTime TimeFrom, DateTime TimeTo, int id)
        {
            return Ok(activityService.GetActivitesByActivityTypePerTime(TimeFrom, TimeTo, id));
        }

        //[Authorize(Roles = Startup.roleAdmin)]
        [Route("api/activitiesbyapprovedbymanager/{TimeFrom}/{TimeTo}/{Id}")]
        public IHttpActionResult GetActivitesByApprovedByManagerPerTime(DateTime TimeFrom, DateTime TimeTo, bool id)
        {
            return Ok(activityService.GetActivitesByApprovedByManagerPerTime(TimeFrom, TimeTo, id));
        }

        //[Authorize(Roles = Startup.roleAdmin)]
        [Route("api/activitesbyprojectrole/{TimeFrom}/{TimeTo}/{Id}")]
        public IHttpActionResult GetActivitesByProjectRoleTypePerTime(DateTime TimeFrom, DateTime TimeTo, int id)
        {
            return Ok(activityService.GetActivitesByProjectRoleTypePerTime(TimeFrom, TimeTo, id));
        }


    }
}
