using System;
using System.Threading.Tasks;
using qsLogPack.Infrastructures.Interfaces;
using qsLogPack.Services.Interfaces;

namespace qsLogPack.Services
{
    internal class LogService : ILogService
    {
        readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public Task<Guid> Error(Exception ex)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Error(string description, Exception ex)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Information(string description)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Warning(string description, Exception ex = null)
        {
            throw new NotImplementedException();
        }
    }
}