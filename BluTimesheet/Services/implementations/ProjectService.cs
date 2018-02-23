using System.Collections.Generic;
using System.Data.Entity;
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

        private readonly TimesheetDbContext context;

        public ProjectService(TimesheetDbContext context)
        {
            this.context = new TimesheetDbContext();
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
            Project entityToDelete = context.Project.Find(id);
            List<Activity> activityToDelete = new List<Activity>();
            var zmienna = context.Activity.Where(i => i.Project.ProjectId == entityToDelete.ProjectId);
            foreach (var item in zmienna)
            {
            context.Activity.Remove(item);
            }
            context.Project.Remove(entityToDelete);
            context.SaveChanges();
        }

        public Project Update(Project project)
        {
            Project projectToEdit = context.Project.Find(project.ProjectId);
            ProjectType projectTypeToEdit = context.ProjectType.Find(project.ProjectType.ProjectTypeId);

            projectToEdit.Name = project.Name;
            projectToEdit.Projectnumber = project.Projectnumber;
            projectToEdit.ProjectType = projectTypeToEdit;
            projectToEdit.Client = project.Client;
            context.Entry(projectToEdit).State = EntityState.Modified;
            context.SaveChanges();

            return projectToEdit;
        }
    }
}