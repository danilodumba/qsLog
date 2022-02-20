using System;
using qsLibPack.Repositories.Mongo;
using qsLibPack.Repositories.Mongo.Core;
using qsLog.Domains.Logs;
using qsLog.Domains.Logs.Repository;

namespace qsLog.Infrastructure.Database.MongoDB.Repositories
{
    public class LogRepository : Repository<Log, Guid>, ILogRepository
    {
        public LogRepository(IMongoContext context) : base(context)
        {
        }
    }
}