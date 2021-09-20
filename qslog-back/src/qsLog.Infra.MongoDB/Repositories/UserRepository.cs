using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qsLibPack.Repositories.Mongo;
using qsLibPack.Repositories.Mongo.Core;
using qsLog.Domains.Users;
using qsLog.Domains.Users.Repository;
using MongoDB.Driver;

namespace qsLog.Infrastructure.Database.MongoDB.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {
        }

        public long Count()
        {
            return _dbSet.CountDocuments(Builders<User>.Filter.Empty);
        }

        public bool ExistsUserName(string userName)
        {
            return _dbSet.CountDocuments(x => x.UserName == userName) > 0;
        }

        public async Task<User> GetByUserName(string userName)
        {
            var users = await _dbSet.FindAsync(x => x.UserName == userName);
            return users.FirstOrDefault();
        }

        public IList<User> List(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return _dbSet.Find(Builders<User>.Filter.Empty).ToList();
            }

            return _dbSet.Find(x => 
                x.Name.Contains(search) ||
                x.UserName.Contains(search)
            ).ToList();
        }

        IEnumerable<User> IUserRepository.ListAll()
        {
            return _dbSet.Find(Builders<User>.Filter.Empty).ToList();
        }
    }
}