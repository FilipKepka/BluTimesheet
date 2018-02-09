using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BluTimesheet.Context;
using BluTimesheet.Models.DbModels;
using BluTimesheet.Repositories;
using BluTimesheet.Services.interfaces;

namespace BluTimesheet.Services.implementations
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectRepository projectRepository;

        private readonly ProjectTypeRepository projectTypeRepository;

        public ProjectService(TimesheetDbContext context)
        {
            this.projectRepository = new ProjectRepository(context);
            this.projectTypeRepository = new ProjectTypeRepository(context);
        }

        public Project Add(Project project)
        {
            var projectsType = projectTypeRepository.GetAll();
            var exactProjectType = projectsType.First(x => x.ProjectTypeId == project.ProjectType.ProjectTypeId);
            project.ProjectType = exactProjectType;

           return projectRepository.Add(project);
        }

        public Project Get(int id)
        {
            return projectRepository.Get(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return projectRepository.GetAll();
        }

        public IEnumerable<Project> GetProjectsByProjectType(int id)
        {
            return projectRepository.Search(x => x.ProjectType.ProjectTypeId == id);
        }

        public void Remove(int id)
        {
            projectRepository.Remove(id);
        }

        public void Update(Project project)
        {
            projectRepository.Update(project);
        }
    }
}