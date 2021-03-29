using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using qsLogPack.Exceptions;
using qsLogPack.Infrastructures.Interfaces;
using qsLogPack.Models;
using Newtonsoft.Json;

namespace qsLogPack.Infrastructures.HttpQsLog
{
    internal class LogHttpRepository : ILogRepository
    {
        private readonly QsLogSettings _logSettings;
        private readonly ILogTxtRespository _logTxtRepository;
        public LogHttpRepository(IOptions<QsLogSettings> logSettings, ILogTxtRespository logTxtRepository)
        {
            if (logSettings == null)
            {
                throw new LogException("Configuracao do qsLog nao informada nos Settings");
            }

            _logSettings = logSettings.Value;
            _logTxtRepository = logTxtRepository;
        }

        public async Task<Guid> Send(LogModel model)
        {
            this.ValidateSettings();
            using var client = new HttpClient();
            try
            {
                
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_logSettings.LogApi + "/api-key=" + _logSettings.ApiKey, content);

                if (response.IsSuccessStatusCode)
                {
                    var logID = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());

                    if (_logSettings.GenerateLogTxt)
                    {
                        _logTxtRepository.Create(model.Source);
                    }

                    return logID;
                }

                var error = await response.Content.ReadAsStringAsync();
                _logTxtRepository.Create(error);
            }

            catch (Exception ex)
            {
                 _logTxtRepository.Create(ex);
                throw;
            }
            finally
            {
                client.Dispose();
            }

            return Guid.Empty;
        }

        private void ValidateSettings()
        {
            if (string.IsNullOrEmpty(_logSettings.ApiKey))
                throw new LogException("Informe uma ApiKey do Projeto");

            if (string.IsNullOrEmpty(_logSettings.LogApi))
                throw new LogException("Informe a API do log");
        }
    }
}