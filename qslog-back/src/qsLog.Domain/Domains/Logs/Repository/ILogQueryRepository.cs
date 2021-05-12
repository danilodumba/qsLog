using System;
using System.Collections.Generic;
using qsLibPack.Domain.ValueObjects.Br;
using qsLog.Domains.Logs.DTO;

namespace qsLog.Domains.Logs.Repository
{
    public interface ILogQueryRepository
    {
        IEnumerable<LogListDTO> List(PeriodoVO period, string description, Guid? projectID, LogTypeEnum? type);
    }
}