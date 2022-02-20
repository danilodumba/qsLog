using System;
using qsLibPack.Repositories.EF;
using qsLog.Domains.Logs;
using qsLog.Domains.Logs.Repository;
using qsLog.Infrastructure.Database.MySql.EF.Contexts;

namespace qsLog.Infrastructure.Database.MySql.EF.Repository
{
    public class LogRepository : Repository<Log, Guid>, ILogRepository
    {
        public LogRepository(LogContext context) : base(context)
        {
        }
    }
}