using System;
using Microsoft.EntityFrameworkCore;
using qsLibPack.Repositories.EF;
using qsLog.Domains.Logs;
using qsLog.Domains.Logs.Repository;
using qsLog.Infrastructure.EF.Contexts;

namespace qsLog.Infrastructure.EF.Repository
{
    public class LogRepository : Repository<Log, Guid>, ILogRepository
    {
        public LogRepository(LogContext context) : base(context)
        {
        }
    }
}