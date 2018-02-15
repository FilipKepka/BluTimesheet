using BluTimesheet.Models.DbModels;
using System.Collections.Generic;

namespace BluTimesheet.Services.interfaces
{
    public interface IProjectService
    {
        Project Add(Project project);
        void Remove(int id);
        Project Get(int id);
        Project Update(Project project);
        IEnumerable<Project> GetAll();
        IEnumerable<Project> GetProjectsByProjectType(int id);
    }
}