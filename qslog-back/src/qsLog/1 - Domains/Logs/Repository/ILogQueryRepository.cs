using System;
using System.Collections.Generic;
using qsLibPack.Domain.ValueObjects.Br;
using qsLog.Domains.Logs.DTO;

namespace qsLog.Domains.Logs.Repository
{
    public interface ILogQueryRepository
    {
        IEnumerable<LogListDTO> List(PeriodoVO periodo, string nome, Guid? projectID, LogTypeEnum? type);
    }
}