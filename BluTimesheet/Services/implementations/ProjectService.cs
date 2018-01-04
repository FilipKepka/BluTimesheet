using System.Collections.Generic;
using System.Threading.Tasks;
using BluTimesheet.Models.DbModels;
using BluTimesheet.Repositories;
using BluTimesheet.Services.interfaces;

namespace BluTimesheet.Services.implementations
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectRepository projectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public void Add(Project project)
        {
            projectRepository.Add(project);
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
            return projectRepository.Search(x => x.ProjectType.Id == id);
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