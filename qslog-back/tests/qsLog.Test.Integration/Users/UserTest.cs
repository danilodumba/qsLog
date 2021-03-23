using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using qsLog.Applications.Models;
using Xunit;

namespace qsLog.Test.Integration.Users
{
    public class UserTest : HostBase
    {
        public UserTest() : base("/api/user")
        {
        }

        [Fact]
        public async Task Deve_Criar_Um_Usuario()
        {
            var model = UserMock.GetUserModel();

            var response = await this.Post("", model);
            Assert.True(response.IsSuccessStatusCode, response.StatusCode.ToString());
        }

        [Fact]
        public async Task Deve_Criar_Um_Usuario_BadRequest()
        {
            var model = UserMock.GetUserModel();
            model.Name = "";

            var response = await this.Post("", model);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest, response.StatusCode.ToString());
        }

        [Fact]
        public async Task Deve_Buscar_Um_Usuario_Por_Id()
        {
            var id = await this.CriarUsuario();

            var response = await this.Get(id.ToString());
            Assert.True(response.IsSuccessStatusCode, response.StatusCode.ToString());

            var user = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());
            Assert.Equal(id, user.Id);
        }

        [Fact]
        public async Task Deve_Buscar_Um_Usuario_Por_Id_NotFound()
        {
            var id = Guid.NewGuid();

            var response = await this.Get(id.ToString());
            Assert.True(response.StatusCode == HttpStatusCode.NotFound, response.StatusCode.ToString());
        }

        [Fact]
        public async Task Deve_Alterar_Um_Usuario()
        {
            var id = await this.CriarUsuario();
            var model = UserMock.GetUserModel();

            var response = await this.Put(id.ToString(), model);
            Assert.True(response.IsSuccessStatusCode, response.StatusCode.ToString());
        }

        [Fact]
        public async Task Deve_Alterar_Um_Usuario_BadRequest()
        {
            var id = await this.CriarUsuario();
            var model = UserMock.GetUserModel();
            model.Name = "";

            var response = await this.Put(id.ToString(), model);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest, response.StatusCode.ToString());
        }

       [Fact]
        public async Task Deve_Listar_Usuarios()
        {
            await this.CriarUsuario();
           
            var response = await this.Get("");
            Assert.True(response.IsSuccessStatusCode, response.StatusCode.ToString());

            var users = JsonConvert.DeserializeObject<List<UserListModel>>(await response.Content.ReadAsStringAsync());

            Assert.True(users.Count > 0, $"Esperado maior que zero.");
        }

        private async Task<Guid> CriarUsuario()
        {
            var model = UserMock.GetUserModel();

            var response = await this.Post("", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao criar o usuario");
            }
            return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
        }
    }
}