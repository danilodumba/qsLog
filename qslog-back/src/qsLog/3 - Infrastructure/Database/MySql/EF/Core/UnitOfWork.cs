using System.Threading.Tasks;
using qsLibPack.Repositories.Interfaces;
using qsLog.Infrastructure.Database.MySql.EF.Contexts;

namespace qsLog.Infrastructure.Database.MySql.EF.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly LogContext _context;

        public UnitOfWork(LogContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}