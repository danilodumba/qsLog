using System;
using System.Threading.Tasks;
namespace qsLog.Domains.Logs.Repository
{
    public interface ILogRepository
    {
        Task<Log> GetByIDAsync(Guid id);
        Task CreateAsync(Log log);
    }
}