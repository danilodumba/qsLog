using System;
using System.Threading.Tasks;

namespace qsLog.Domain.Applications.Services.Interfaces
{
    public interface IServiceBus: IDisposable
    {
        Task Consumer();
    }
}