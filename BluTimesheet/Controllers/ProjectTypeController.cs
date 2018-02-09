using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using System.Web.Http;
using BluTimesheet.Models.DbModels;
using System.Threading.Tasks;

namespace BluTimesheet.Controllers
{
    //[Authorize(Roles = Startup.roleAdmin)]
    public class ProjectTypeController : ApiController
    {
        private IProjectTypeService projectTypeService;

        public ProjectTypeController(IProjectTypeService projectTypeService)
        {
            this.projectTypeService = projectTypeService;
        }

    
        public IHttpActionResult PostProjectType(ProjectType projectType)
        {
            projectTypeService.Add(projectType);
            return Ok();
        }

        public IEnumerable<ProjectType> GetProjectTypes()
        {
            return projectTypeService.GetAll();
        }

        public IHttpActionResult GetProjectType(int id)
        {
            var projectType = projectTypeService.Get(id);
            if (projectType != null)
            {
                return Ok(projectType);
            } else
            {
                return NotFound();
            }
            
        }

        public IHttpActionResult PutProjectType(ProjectType projectType)
        {
            var projectTypeFromDb = projectTypeService.Get(projectType.ProjectTypeId);
            if (projectTypeFromDb != null)
            {
                projectTypeService.Update(projectType);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult DeleteProjectType(int id)
        {
            var projectType = projectTypeService.Get(id);
            if (projectType != null)
            {
                projectTypeService.Remove(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
