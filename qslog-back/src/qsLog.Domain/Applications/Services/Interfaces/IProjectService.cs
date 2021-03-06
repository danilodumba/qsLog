using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qsLog.Applications.Models;

namespace qsLog.Applications.Services.Interfaces
{
    public interface IProjectService
    {
        Task<Guid> Create(ProjectModel model);
        Task Update(ProjectModel model, Guid id);
        Task<ProjectModel> GetByID(Guid id);
        IList<ProjectListModel> ListAll();
        IList<ProjectListModel> ListByName(string name);
        Task Remove(Guid id);
    }
}