using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using qsLogPack.Exceptions;
using qsLogPack.Infrastructures.Interfaces;
using qsLogPack.Models;
using qsLogPack.Services.Interfaces;

namespace qsLogPack.Services
{
    internal class LogService : ILogService
    {
        readonly QsLogSettings _logSettings;
        readonly ILogRepository _logRepository;
        readonly ILogTxtRespository _logTxtRepository;

        public LogService(IOptions<QsLogSettings> logSettings, ILogRepository logRepository, ILogTxtRespository logTxtRepository)
        {
            if (logSettings == null)
            {
                throw new LogException("Configuracao do qsLog nao informada nos Settings");
            }

            _logSettings = logSettings.Value;
            _logRepository = logRepository;
            _logTxtRepository = logTxtRepository;
        }

        public async Task<Guid> Error(Exception ex)
        {
            return await Error(null, ex);
        }

        public async Task<Guid> Error(string description, Exception ex)
        {
            var model = new LogModel(description ?? ex.Message, this.GetSource(ex), LogTypeEnum.Error);
            try
            {
                var id = await _logRepository.Send(model);

                if (_logSettings.GenerateLogTxt)
                {
                    this.CreateLogTxt(model);
                }

                return id;
            }
            catch (LogException lx)
            {
                _logTxtRepository.Create(ex);
                _logTxtRepository.Create(lx);
                throw lx;
            }
        }

        public async Task<Guid> Information(string description, string source = "")
        {
            var model = new LogModel(description, source, LogTypeEnum.Information);
            try
            {
                var id = await _logRepository.Send(model);

                if (_logSettings.GenerateLogTxt)
                {
                    this.CreateLogTxt(model);
                }

                return id;
            }
            catch (LogException lx)
            {
                 _logTxtRepository.Create(description);
                 _logTxtRepository.Create(lx);
                 throw lx;
            }
        }

        public async Task<Guid> Warning(string description, Exception ex = null)
        {
            var model = new LogModel(description, this.GetSource(ex), LogTypeEnum.Warning);
            try
            {
                var id = await _logRepository.Send(model);

                if (_logSettings.GenerateLogTxt)
                {
                    this.CreateLogTxt(model);
                }

                return id;
            }
            catch (LogException lx)
            {
                if (ex != null)
                    _logTxtRepository.Create(ex);
                else 
                    _logTxtRepository.Create(description);

                 _logTxtRepository.Create(lx);
                 throw lx;
            }
        }

        private string GetSource(Exception ex)
        {
            if (ex == null) return string.Empty;
            
            var source = ex.Message + System.Environment.NewLine;
            if (ex.InnerException != null)
            {
                source = GetSource(ex.InnerException);
            }

            return source;
        }

        private void CreateLogTxt(LogModel model)
        {
            string log = model.Description + System.Environment.NewLine + model.Source;
            _logTxtRepository.Create(log);
        }
    }
}