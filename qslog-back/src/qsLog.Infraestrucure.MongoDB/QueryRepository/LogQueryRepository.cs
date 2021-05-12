using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using qsLibPack.Domain.ValueObjects.Br;
using qsLibPack.Repositories.Mongo.Core;
using qsLog.Domains.Logs;
using qsLog.Domains.Logs.DTO;
using qsLog.Domains.Logs.Repository;

namespace qsLog.Infrastructure.Database.MongoDB.QueryRepository
{
    public class LogQueryRepository : ILogQueryRepository
    {
        readonly IMongoCollection<Log> _dbSet;

        public LogQueryRepository(IMongoContext context)
        {
            _dbSet = context.GetCollection<Log>(typeof(Log).Name);
        }

        //TODO: Implementar o restante dos filtros. Por enquanto esta somente para testes e aprendizado. 
        public IEnumerable<LogListDTO> List(PeriodoVO period, string description, Guid? projectID, LogTypeEnum? type)
        {
            var logs = _dbSet.Find(x => x.Creation >= period.DataInicial && x.Creation <= period.DataFinal).ToList();

            var dto = logs.Select(l => new LogListDTO
            {
                Description = l.Description,
                Project = l.Project.Name,
                Id = l.Id,
                Creation = l.Creation,
                LogType = (int)l.LogType
            });

            return dto;
        }
    }
}