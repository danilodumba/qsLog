using System;
using System.Collections.Generic;
using qsLibPack.Domain.ValueObjects.Br;
using qsLog.Domains.Logs;
using qsLog.Domains.Logs.DTO;
using qsLog.Domains.Logs.Repository;

namespace qsLog.Test.Integration.Logs
{
    public class LogQueryRepositoryInMemoryMock : ILogQueryRepository
    {
        public IEnumerable<LogListDTO> List(PeriodoVO period, string description, Guid? projectID, LogTypeEnum? type)
        {
            var list = new List<LogListDTO>
            {
                new LogListDTO
                {
                    Description = "Description",
                    LogType = (int)LogTypeEnum.Error,
                    Project = "Project",
                    Id = Guid.NewGuid(),
                    Creation = DateTime.Now
                }
            };

            return list;
        }
    }
}