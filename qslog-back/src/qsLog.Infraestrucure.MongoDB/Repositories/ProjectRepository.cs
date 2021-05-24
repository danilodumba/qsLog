using System;
using System.Collections.Generic;
using MongoDB.Driver;
using qsLibPack.Repositories.Mongo;
using qsLibPack.Repositories.Mongo.Core;
using qsLog.Domains.Projects;
using qsLog.Domains.Projects.Repository;

namespace qsLog.Infrastructure.Database.MongoDB.Repositories
{
    public class ProjectRepository : Repository<Project, Guid>, IProjectRepository
    {

        public ProjectRepository(IMongoContext context) : base(context)
        {
        }

        public bool ApiKeyExists(Guid apiKey)
        {
            return _dbSet.CountDocuments(x => x.ApiKey == apiKey) > 0;
        }

        public Project GetByApiKey(Guid apiKey)
        {
            return _dbSet.Find(Builders<Project>.Filter.Eq("ApiKey", apiKey)).FirstOrDefault();
            //return _dbSet.Find(x => x.ApiKey == apiKey).FirstOrDefault();
        }

        IEnumerable<Project> IProjectRepository.ListAll()
        {
            return _dbSet.Find(Builders<Project>.Filter.Empty).ToList();
        }
    }
}