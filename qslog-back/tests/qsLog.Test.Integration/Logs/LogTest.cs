using System.Collections.Generic;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using qsLog.Presentetion.Models;
using qsLog.Test.Integration.Projects;
using Xunit;
using qsLog.Domains.Logs.DTO;

namespace qsLog.Test.Integration.Logs
{
    public class LogTest: HostBase
    {
        public LogTest() : base("/api/log")
        {
            
        }

        [Fact]
        public async Task Deve_Criar_Log()
        {
            var model = LogMock.GetLogModel();

            var response = await this.Post($"?api-key={this.ObterApiKeyProjeto()}", model, false);

            string error = "";
            if (!response.IsSuccessStatusCode)
            {
                error = await response.Content.ReadAsStringAsync();
            }

            Assert.True(response.IsSuccessStatusCode, error); 
        }

        [Fact]
        public async Task Deve_Criar_Log_BadRequest()
        {
            var model = new LogModel();

            var response = await this.Post($"?api-key={this.ObterApiKeyProjeto()}", model);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest, response.StatusCode.ToString()); 
        }

        [Fact]
        public async Task Deve_Criar_Log_Unauthorized()
        {
            var model = new LogModel();

            var response = await this.Post($"?api-key={Guid.NewGuid()}", model);

            Assert.True(response.StatusCode == HttpStatusCode.Unauthorized, response.StatusCode.ToString()); 
        }

        [Fact]
        public async Task Deve_Obter_LogByID()
        {
            var id = await this.CriarLog();

            var response = await this.Get(id.ToString());
            string error = "";
            if (!response.IsSuccessStatusCode)
            {
                error = await response.Content.ReadAsStringAsync();
            }

            Assert.True(response.IsSuccessStatusCode, error); 
        }

        [Fact]
        public async Task Deve_Obter_LogByID_NotFound()
        {
            var id = Guid.NewGuid();

            var response = await this.Get(id.ToString());

            Assert.True(response.StatusCode == HttpStatusCode.NotFound, response.StatusCode.ToString()); 
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Por_Periodo()
        {
            await this.CriarLog();
            var dataInicial = DateTime.Now.ToString("yyyy-MM-dd");
            var dataFinal = DateTime.Now.Date.AddHours(23).ToString("yyyy-MM-dd hh:mm:ss");

            var response = await this.Get($"{dataInicial}/{dataFinal}");
            string error = "";
            if (!response.IsSuccessStatusCode)
            {
                error = await response.Content.ReadAsStringAsync();
            }

            Assert.True(response.IsSuccessStatusCode, error); 

            var list = JsonConvert.DeserializeObject<List<LogListDTO>>(await response.Content.ReadAsStringAsync());
            Assert.True(list.Count > 0, "Deve retornar uma lista de logs maior que zero.");
        }

        // [Fact]
        // public async Task Deve_Retornar_Lista_Por_Description()
        // {

        //     var model = new LogModel
        //     {
        //         Description = "Teste description",
        //         Source = "Source adsdlasjd ajsdh kj jahs dkjhs akjdh kasjhdjk asdh kj",
        //         LogType = Domains.Logs.LogTypeEnum.Error
        //     };

        //     await this.CriarLog(model);
        //     var dataInicial = DateTime.Now.ToString("yyyy-MM-dd");
        //     var dataFinal = DateTime.Now.ToString("yyyy-MM-dd");;

        //     var response = await this.Get($"{dataInicial}/{dataFinal}/?description=description");
        //     string error = "";
        //     if (!response.IsSuccessStatusCode)
        //     {
        //         error = await response.Content.ReadAsStringAsync();
        //     }

        //     Assert.True(response.IsSuccessStatusCode, error); 

        //     var list = JsonConvert.DeserializeObject<List<LogListDTO>>(await response.Content.ReadAsStringAsync());
        //     Assert.True(list.Count > 0, "Deve retornar uma lista de logs maior que zero.");
        // }

        // [Fact]
        // public async Task Deve_Retornar_Lista_Por_Source()
        // {

        //     var model = new LogModel
        //     {
        //         Description = "Teste description",
        //         Source = "Source adsdlasjd ajsdh kj jahs dkjhs akjdh kasjhdjk asdh kj",
        //         LogType = Domains.Logs.LogTypeEnum.Error
        //     };

        //     await this.CriarLog(model);
        //     var dataInicial = DateTime.Now.ToString("yyyy-MM-dd");
        //     var dataFinal = DateTime.Now.ToString("yyyy-MM-dd");;

        //     var response = await this.Get($"{dataInicial}/{dataFinal}/?description=Source");
        //     string error = "";
        //     if (!response.IsSuccessStatusCode)
        //     {
        //         error = await response.Content.ReadAsStringAsync();
        //     }

        //     Assert.True(response.IsSuccessStatusCode, error); 

        //     var list = JsonConvert.DeserializeObject<List<LogListDTO>>(await response.Content.ReadAsStringAsync());
        //     Assert.True(list.Count > 0, "Deve retornar uma lista de logs maior que zero.");
        // }

        // [Fact]
        // public async Task Deve_Retornar_Lista_Por_LogType()
        // {

        //     var model = new LogModel
        //     {
        //         Description = "Teste description",
        //         Source = "Source adsdlasjd ajsdh kj jahs dkjhs akjdh kasjhdjk asdh kj",
        //         LogType = Domains.Logs.LogTypeEnum.Information
        //     };

        //     await this.CriarLog(model);
        //     var dataInicial = DateTime.Now.ToString("yyyy-MM-dd");
        //     var dataFinal = DateTime.Now.ToString("yyyy-MM-dd");;

        //     var response = await this.Get($"{dataInicial}/{dataFinal}/?type=" + model.LogType);
        //     string error = "";
        //     if (!response.IsSuccessStatusCode)
        //     {
        //         error = await response.Content.ReadAsStringAsync();
        //     }

        //     Assert.True(response.IsSuccessStatusCode, error); 

        //     var list = JsonConvert.DeserializeObject<List<LogListDTO>>(await response.Content.ReadAsStringAsync());
        //     Assert.True(list.Count > 0, "Deve retornar uma lista de logs maior que zero.");
        // }


        private async Task<Guid> CriarLog()
        {
            var model = LogMock.GetLogModel();

            var response = await this.Post($"?api-key={this.ObterApiKeyProjeto()}", model, false);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao criar log");
            }

           return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
        }

        private async Task<Guid> CriarLog(LogModel model)
        {
            var response = await this.Post($"?api-key={this.ObterApiKeyProjeto()}", model, false);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao criar log");
            }

           return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
        }

        private Guid ObterApiKeyProjeto()
        {
            var projetoTest = new ProjectTest();
            var model = projetoTest.ObterProjeto().Result;
            return model.ApiKey;
        }

        private Guid ObterIdProjeto()
        {
            var projetoTest = new ProjectTest();
            var model = projetoTest.ObterProjeto().Result;
            return model.Id;
        }
       
    }
}