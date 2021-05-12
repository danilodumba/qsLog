using System.Linq;
using qsLog.Domains.Logs.Repository;
using qsLibPack.Repositories.EF;
using System.Collections.Generic;
using qsLog.Domains.Logs.DTO;
using qsLibPack.Domain.ValueObjects.Br;
using System;
using qsLog.Domains.Logs;
using qsLog.Infrastructure.Database.MySql.EF.Contexts;
using MySqlConnector;

namespace qsLog.Infrastructure.Database.MySql.EF.QueryRepositories
{
    public class LogQueryRepository : QueryRepository, ILogQueryRepository
    {
        public LogQueryRepository(LogContext context) : base(context)
        {
    
        }

        public IEnumerable<LogListDTO> List(PeriodoVO period, string description, Guid? projectID, LogTypeEnum? type)
        {
            var periodoFinal = period.DataFinal.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            var sql = $@"select
                        l.id, 
                        l.description,
                        l.creation,
                        l.log_type as LogType,
                        p.name as Project 
                            from logs l
                            join projects p on l.project_id = p.id
                        where 
                            creation >= '{period.DataInicial:yyyy-MM-dd}' and creation <= '{periodoFinal:yyyy-MM-dd HH:mm:ss}'
                            and (@description is null or (description like @description or source like @description))
                            and (@projectID is null or l.project_id = @projectID)
                            and (@type is null or log_type = @type)
                        order by creation desc
                        limit 100";
            
            var parameters = new List<object>();
            if (!string.IsNullOrWhiteSpace(description))
            {
                var parameter = new MySqlParameter("@description", $"%{description}%");
                parameters.Add(parameter);
            }

            if (projectID.HasValue)
            {
                var parameter = new MySqlParameter("@projectID", projectID.Value);
                parameters.Add(parameter);
            }

            if (type.HasValue)
            {
                var parameter = new MySqlParameter("@type", (int)type.Value);
                parameters.Add(parameter);
            }
            
            return this.SelectSql<LogListDTO>(sql, parameters.ToArray());
        }
    }
}