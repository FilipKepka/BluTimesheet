using BluTimesheet.Models.DbModels;
using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace BluTimesheet.Controllers
{
    // [Authorize(Roles =Startup.roleAdmin)]
    public class ProjectRoleTypeController : ApiController
    {
        private IProjectRoleTypeService projectRoleTypeService;

        public ProjectRoleTypeController(IProjectRoleTypeService projectRoleTypeService)
        {
            this.projectRoleTypeService = projectRoleTypeService;
        }

        public IHttpActionResult PostProjectRoleType(ProjectRoleType projectRoleType)
        {
            projectRoleTypeService.Add(projectRoleType);
            return Ok();
        }

        [HttpGet]
        public IEnumerable<ProjectRoleType> ProjectRoleType()
        {
            var result =  projectRoleTypeService.GetAll(); // 
            return result;
        }

        public IHttpActionResult GetProjectRoleType(int id)
        {
            var role = projectRoleTypeService.Get(id);
            if (role != null)
            {

                return Ok(role);
            }
            else
                return NotFound();
        }

        public IHttpActionResult PutProjectRoleType(ProjectRoleType publicRoleType)
        {
            var publicRoleTypeDb = projectRoleTypeService.Get(publicRoleType.Id);
            if (publicRoleTypeDb != null)
            {
                projectRoleTypeService.Update(publicRoleType);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult DeleteProjectRoleType(int id)
        {
            var publicRoleTypeDb = projectRoleTypeService.Get(id);
            if (publicRoleTypeDb != null)
            {
                projectRoleTypeService.Remove(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}