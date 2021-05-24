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
            var endDate = period.DataFinal.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var builder = Builders<Log>.Filter;
            FilterDefinition<Log> filter = builder.Gte(x => x.Creation, period.DataInicial.Date) & builder.Lte(x => x.Creation, endDate);

            if (!string.IsNullOrWhiteSpace(description))
            {
                var queryExpr = new BsonRegularExpression(new Regex(description, RegexOptions.None));
                filter = filter & (builder.Regex("Description", queryExpr) | builder.Regex("Source", queryExpr));
            }

            if (projectID.HasValue)
            {
                filter = filter & builder.Eq("ProjectID", projectID.Value);
            }

            if (type.HasValue)
            {
                filter = filter & builder.Eq("LogType", type.Value);
            }


            var logs = _dbSet.Find(filter).ToList();

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