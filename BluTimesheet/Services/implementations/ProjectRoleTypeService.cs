using BluTimesheet.Models.DbModels;
using BluTimesheet.Repositories;
using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluTimesheet.Services.implementations
{
    public class ProjectRoleTypeService : IProjectRoleTypeService
    {
        private readonly ProjectRoleTypeRepository projectRoleTypeRepository;

        public ProjectRoleTypeService(ProjectRoleTypeRepository projectRoleTypeRepository)
        {
            this.projectRoleTypeRepository = projectRoleTypeRepository;
        }

        public ProjectRoleType Add(ProjectRoleType projectRoleType)
        {
            return projectRoleTypeRepository.Add(projectRoleType);
        }

        public  ProjectRoleType Get(int id)
        {
            var result = projectRoleTypeRepository.Get(id);
            return result;
        }

        public IEnumerable<ProjectRoleType> GetAll()
        {
            var result = projectRoleTypeRepository.GetAll();
            return result;
        }

        public void Remove(int id)
        {
            projectRoleTypeRepository.Remove(id);
        }

        public void Update(ProjectRoleType projectRoleType)
        {
            projectRoleTypeRepository.Update(projectRoleType);
        }
    }
}