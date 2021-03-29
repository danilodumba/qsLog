using System;
using System.Threading.Tasks;
namespace qsLogPack.Services.Interfaces
{
    public interface ILogService
    {
        Task<Guid> Information(string description, string source = "");
        Task<Guid> Warning(string description, Exception ex = null);
        Task<Guid> Error(Exception ex);
        Task<Guid> Error(string description, Exception ex);
    }
}