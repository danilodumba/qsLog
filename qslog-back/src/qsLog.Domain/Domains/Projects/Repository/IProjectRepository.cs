using System;
using System.Collections.Generic;
using qsLibPack.Repositories.Interfaces;

namespace qsLog.Domains.Projects.Repository
{
    public interface IProjectRepository: IRepository<Project, Guid>
    {
        IEnumerable<Project> ListAll();
        bool ApiKeyExists(Guid apiKey);
        Project GetByApiKey(Guid apiKey);
        IEnumerable<Project> ListByName(string name);
    }
}