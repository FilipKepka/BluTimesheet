using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using System.Web.Http;
using BluTimesheet.Models.DbModels;
using System.Threading.Tasks;

namespace BluTimesheet.Controllers
{
    //[Authorize(Roles = Startup.roleAdmin)]
    [Authorize]
    public class ProjectController : ApiController
    {
        private IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult PostProject(Project project)
        {
            var returnData = projectService.Add(project);
            return Ok(returnData);
        }

        public IEnumerable<Project> GetProjects()
        {
            return projectService.GetAll();
        }

        [Route("api/projecttype/{id}/projects")]
        public IEnumerable<Project> GetProjectsByProjectType(int id)
        {
            return projectService.GetProjectsByProjectType(id);
        }

        public IHttpActionResult GetProject(int id)
        {
            var project = projectService.Get(id);
            if (project != null)
            {
                return Ok(project);
            }
            else
            {
                return NotFound();
            }

        }

        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult PutProject(Project project)
        {
            //var projectFromDb = projectService.Get(project.ProjectId);
            if (project != null)
            {
                var returnData = projectService.Update(project);
                return Ok(returnData);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult DeleteProject(int id)
        {
            var projectFromDb = projectService.Get(id);
            if (projectFromDb != null)
            {
                projectService.Remove(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
