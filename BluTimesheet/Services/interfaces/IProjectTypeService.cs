using BluTimesheet.Models.DbModels;
using System.Collections.Generic;

namespace BluTimesheet.Services.interfaces
{
    public interface IProjectTypeService
    {
        void Add(ProjectType projectType);
        void Remove(int id);
        ProjectType Get(int id);
        ProjectType Update(ProjectType projectType);
        IEnumerable<ProjectType> GetAll();

    }
}
