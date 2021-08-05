using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using qsLibPack.Domain.ValueObjects.Br;
using qsLibPack.Repositories.Mongo.Core;
using qsLog.Domains.Logs;
using qsLog.Domains.Logs.DTO;
using qsLog.Domains.Logs.Repository;
using System.Linq.Expressions;

namespace qsLog.Infrastructure.Database.MongoDB.QueryRepository
{
    public class LogQueryRepository : ILogQueryRepository
    {
        readonly IQueryable<Log> _dbSet;

        public LogQueryRepository(IMongoContext context)
        {
            _dbSet = context.GetCollection<Log>(typeof(Log).Name).AsQueryable();
        }

        //TODO: Implementar o restante dos filtros. Por enquanto esta somente para testes e aprendizado. 
        public IEnumerable<LogListDTO> List(PeriodoVO period, string description, Guid? projectID, LogTypeEnum? type)
        {
            var startDate = period.DataInicial.Date;
            var endDate = period.DataFinal.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var logs = _dbSet
                .Where(x => x.Creation >= startDate && x.Creation <= endDate);

            if (!string.IsNullOrWhiteSpace(description))
            {
                logs = logs.Where(x => x.Description.Contains(description));
            }

            if (projectID.HasValue)
            {
                logs = logs.Where(x => x.Project.Id == projectID.Value);
            }

            if (type.HasValue)
            {
                logs = logs.Where(x => x.LogType == type.Value);
            }

            var dto = logs
                .OrderByDescending(x => x.Creation)
                .Take(50)
                .ToList()
                .Select(l => new LogListDTO
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