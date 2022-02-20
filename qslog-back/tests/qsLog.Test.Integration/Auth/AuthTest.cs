using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using qsLog.Applications.Models;
using qsLog.Presentetion.Models;
using qsLog.Test.Integration.Users;
using Xunit;

namespace qsLog.Test.Integration.Auth
{
    public class AuthTest : HostBase
    {
        public AuthTest() : base("api/auth")
        {
        }

        [Fact]
        public async Task Deve_Logar()
        {
            var model = await CriarUsuario();
            
            var loginModel = new LoginModel
            {
                UserName = model.UserName,
                Password = "123456"
            };

            var response = await this.Post("", loginModel);

            Assert.True(response.IsSuccessStatusCode, response.StatusCode.ToString());

            var usuario = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

            Assert.True(usuario.userName == model.UserName);
        }

        [Fact]
        public async Task Deve_Nao_Logar_Com_UserNameInvalido()
        {
            var model = await CriarUsuario();
            
            var loginModel = new LoginModel
            {
                UserName = "joao.abc.d",
                Password = "123456"
            };

            var response = await this.Post("", loginModel);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound, response.StatusCode.ToString());
        }

        [Fact]
        public async Task Deve_Nao_Logar_Com_PasswordInvalido()
        {
            var model = await CriarUsuario();
            
            var loginModel = new LoginModel
            {
                UserName = model.UserName,
                Password = "a@321123A#"
            };

            var response = await this.Post("", loginModel);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound, response.StatusCode.ToString());
        }

        private async Task<UserModel> CriarUsuario()
        {
            var userTest = new UserTest();
            return await userTest.CriarUsuarioRetornandoModel();
        }
    }
}