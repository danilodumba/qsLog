using qsLog.Domains.Logs.Repository;
using qsLibPack.Repositories.EF;
using System.Collections.Generic;
using qsLog.Domains.Logs.DTO;
using qsLibPack.Domain.ValueObjects.Br;
using System;
using qsLog.Domains.Logs;
using qsLog.Infrastructure.EF.Contexts;

namespace qsLog.Infrastructure.EF.QueryRepositories
{
    public class LogQueryRepository : QueryRepository, ILogQueryRepository
    {
        public LogQueryRepository(LogContext context) : base(context)
        {
        }

        public IEnumerable<LogListDTO> List(PeriodoVO periodo, string nome, Guid? projectID, LogTypeEnum? type)
        {
            var sql = $@"select
                        l.id, 
                        l.description,
                        l.creation,
                        l.log_type,
                        p.name as Project 
                            from log l 
                            join project p on l.projectID = p.id
                        where 
                            creation >= '{periodo.DataInicial}' and creation <= '{periodo.DataFinal}'
                            and (@description is null or description like @description)
                            and (@projectID is null or l.project_id = @projectID)
                            and (@type is null or log_type = @type)
                        order by creation desc
                        limit 500";
            
            return this.SelectSql<LogListDTO>(sql);
        }
    }
}