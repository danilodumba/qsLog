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
    internal class LogHttpRepository : ILogRepository, IDisposable
    {
        private readonly QsLogSettings _logSettings;
        public LogHttpRepository(IOptions<QsLogSettings> logSettings)
        {
            if (logSettings == null)
            {
                throw new LogException("Configuracao do qsLog nao informada nos Settings");
            }

            _logSettings = logSettings.Value;
        }

        public async Task<Guid> Send(LogModel model)
        {
            this.ValidateSettings();
            using var client = new HttpClient();
            try
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_logSettings.LogApi + "/?api-key=" + _logSettings.ApiKey, content);

                if (response.IsSuccessStatusCode)
                {
                    var logID = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());

                    return logID;
                }

                var error = await response.Content.ReadAsStringAsync();
                throw new LogException(error);
            }

            catch (Exception ex)
            {
                throw new LogException("Erro ao conectar a API do log", ex);
            }
            finally
            {
                client.Dispose();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
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