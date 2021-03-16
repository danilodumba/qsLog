using System;
using System.Collections.Generic;
using qsLibPack.Repositories.Interfaces;

namespace qsLog.Domains.Projects.Repository
{
    public interface IProjectRepository: IRepository<Project, Guid>
    {
         IEnumerable<Project> ListAll();
    }
}