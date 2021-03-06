using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qsLibPack.Repositories.EF;
using qsLog.Domains.Users;
using qsLog.Domains.Users.Repository;
using qsLog.Infrastructure.Database.MySql.EF.Contexts;

namespace qsLog.Infrastructure.Database.MySql.EF.Repository
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(LogContext context) : base(context)
        {
        }

        public long Count()
        {
            return _dbSet.Count();
        }

        public bool ExistsUserName(string userName)
        {
            return _dbSet.Any(x => x.UserName == userName);
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public IList<User> List(string search)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> ListAll()
        {
            return _dbSet.OrderBy(x => x.Name).AsNoTracking();
        }
    }
}