using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using qsLibPack.Repositories.Interfaces;

namespace qsLog.Domains.Users.Repository
{
    public interface IUserRepository: IRepository<User, Guid>
    {
        long Count();
        IEnumerable<User> ListAll();
        Task<User> GetByUserName(string userName);
        bool ExistsUserName(string userName);
    }
}