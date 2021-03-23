using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using qsLibPack.Repositories.EF;
using qsLog.Domains.Projects;
using qsLog.Domains.Projects.Repository;
using qsLog.Infrastructure.EF.Contexts;

namespace qsLog.Infrastructure.EF.Repository
{
    public class ProjectRepository : Repository<Project, Guid>, IProjectRepository
    {
        public ProjectRepository(LogContext context) : base(context)
        {
        }

        public IEnumerable<Project> ListAll()
        {
            return _dbSet.OrderBy(x => x.Name).AsNoTracking();
        }
    }
}