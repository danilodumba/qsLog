using System;
using System.Threading.Tasks;
using qsLogPack.Models;

namespace qsLogPack.Infrastructures.Interfaces
{
    internal interface ILogRepository
    {
        Task<Guid> Send(LogModel model); 
    }
}