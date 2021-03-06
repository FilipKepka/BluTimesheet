﻿using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using System.Web.Http;
using BluTimesheet.Models.DbModels;
using System.Threading.Tasks;

namespace BluTimesheet.Controllers
{
    //[Authorize(Roles = Startup.roleAdmin)]
    [Authorize]
    public class ProjectTypeController : ApiController
    {
        private IProjectTypeService projectTypeService;

        public ProjectTypeController(IProjectTypeService projectTypeService)
        {
            this.projectTypeService = projectTypeService;
        }

        [Authorize(Roles = "Admin, Manager")]
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

        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult PutProjectType(ProjectType projectType)
        {
            //var projectTypeFromDb = projectTypeService.Get(projectType.ProjectTypeId);
            if (projectType != null)
            {
                var returnData = projectTypeService.Update(projectType);
                return Ok(returnData);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin, Manager")]
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
