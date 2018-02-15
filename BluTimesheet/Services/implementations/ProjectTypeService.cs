using BluTimesheet.Repositories;
using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using BluTimesheet.Models.DbModels;
using System.Threading.Tasks;
using BluTimesheet.Context;
using System.Data.Entity;

namespace BluTimesheet.Services.implementations
{
    public class ProjectTypeService : IProjectTypeService
    {
        private readonly ProjectTypeRepository projectTypeRepository;

        private readonly TimesheetDbContext context;

        public ProjectTypeService(TimesheetDbContext context)
        {
            this.context = new TimesheetDbContext();
            this.projectTypeRepository = new ProjectTypeRepository(context);
        }

        public void Add(ProjectType projectType)
        {
            projectTypeRepository.Add(projectType);
        }

        public ProjectType Get(int id)
        {
            return projectTypeRepository.Get(id);
        }

        public IEnumerable<ProjectType> GetAll()
        {
            return projectTypeRepository.GetAll();
        }

        public void Remove(int id)
        {
            projectTypeRepository.Remove(id);
        }

        public ProjectType Update(ProjectType projectType)
        {
            ProjectType projectTypeToEdit = context.ProjectType.Find(projectType.ProjectTypeId);

            projectTypeToEdit.Name = projectType.Name;

            context.Entry(projectTypeToEdit).State = EntityState.Modified;
            context.SaveChanges();

            return projectTypeToEdit;
          //  projectTypeRepository.Update(projectType);
        }
    }
}