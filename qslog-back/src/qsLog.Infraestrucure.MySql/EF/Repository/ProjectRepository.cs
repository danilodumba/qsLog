using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using qsLibPack.Repositories.EF;
using qsLog.Domains.Projects;
using qsLog.Domains.Projects.Repository;
using qsLog.Infrastructure.Database.MySql.EF.Contexts;

namespace qsLog.Infrastructure.Database.MySql.EF.Repository
{
    public class ProjectRepository : Repository<Project, Guid>, IProjectRepository
    {
        public ProjectRepository(LogContext context) : base(context)
        {
        }

        public bool ApiKeyExists(Guid apiKey)
        {
            return _dbSet.Any(x => x.ApiKey == apiKey);
        }

        public IEnumerable<Project> ListAll()
        {
            return _dbSet.OrderBy(x => x.Name).AsNoTracking();
        }

        public Project GetByApiKey(Guid apiKey)
        {
            return _dbSet.FirstOrDefault(x => x.ApiKey == apiKey);
        }
    }
}