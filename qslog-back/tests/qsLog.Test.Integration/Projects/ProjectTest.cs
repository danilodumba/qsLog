using System.Collections.Generic;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using qsLog.Applications.Models;
using Xunit;

namespace qsLog.Test.Integration.Projects
{
    public class ProjectTest: HostBase
    {
        public ProjectTest() : base("/api/project")
        {
        }

        [Fact]
        public async Task Deve_Criar_Um_Projeto()
        {
            var model = ProjectMock.GetProjectModel();

            var response = await this.Post("", model);
            Assert.True(response.IsSuccessStatusCode, response.StatusCode.ToString());   
        }

        [Fact]
        public async Task Deve_Criar_Um_Projeto_BadRequest()
        {
            var model = new ProjectModel();

            var response = await this.Post("", model);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest, response.StatusCode.ToString());
        }

        [Fact]
        public async Task Deve_Alterar_Projeto()
        {
            var id = await this.CriarProjeto();
            var model = ProjectMock.GetProjectModel();
            model.Name = "Alterado - " + model.Name;

            var response = await this.Put(id.ToString(), model);
            string error = "";
            if (!response.IsSuccessStatusCode)
            {
                error = await response.Content.ReadAsStringAsync();
            }

            Assert.True(response.IsSuccessStatusCode, error);
        }

        [Fact]
        public async Task Deve_Alterar_Projeto_BadRequest()
        {
            var id = await this.CriarProjeto();
            var model = new ProjectModel
            {
                Name = ""
            };

            var response = await this.Put(id.ToString(), model);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest, response.StatusCode.ToString());
        }


        [Fact]
        public async Task Deve_Obter_Projeto_Por_Id()
        {
            var id = await this.CriarProjeto();

            var response = await this.Get(id.ToString());
            string error = "";

            if (!response.IsSuccessStatusCode)
            {
                error = await response.Content.ReadAsStringAsync();
            }

            Assert.True(response.IsSuccessStatusCode, error);

            var model = JsonConvert.DeserializeObject<ProjectModel>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(model);
        }

        [Fact]
        public async Task Deve_Obter_Projeto_Por_Id_NotFound()
        {
            var id = Guid.NewGuid();

            var response = await this.Get(id.ToString());
            
            Assert.True(response.StatusCode == HttpStatusCode.NotFound, response.StatusCode.ToString());
        }


        [Fact]
        public async Task Deve_Listar_Projetos()
        {
            await this.CriarProjeto();

            var response = await this.Get("");
            string error = "";

            if (!response.IsSuccessStatusCode)
            {
                error = await response.Content.ReadAsStringAsync();
            }

            Assert.True(response.IsSuccessStatusCode, error);

            var list = JsonConvert.DeserializeObject<List<ProjectListModel>>(await response.Content.ReadAsStringAsync());

            Assert.True(list.Count > 0, "Deve retornar uma lista de projetos maior que zero");
        }


        public async Task<Guid> CriarProjeto()
        {
            var model = ProjectMock.GetProjectModel();

            var response = await Post("", model);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro o criar um projeto");
            }

            return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ProjectModel> ObterProjeto()
        {
            var id = await this.CriarProjeto();

            var response = await this.Get(id.ToString());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Projeto nao encontrado");
            }

            var model = JsonConvert.DeserializeObject<ProjectModel>(await response.Content.ReadAsStringAsync());

            return model;
        }
    }
}