using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using qsLog.Infrastructure.Database.MySql.EF.Contexts;
using System.Linq;
using Microsoft.Extensions.Configuration;
using qsLog.Domains.Logs.Repository;
using qsLog.Test.Integration.Logs;
using qsLog.Presentetion.Models;

namespace qsLog.Test.Integration
{
    public abstract class HostBase
    {
        protected readonly HttpClient _client;
        private readonly string _api;
        const string ENVIRONMENT = "Staging";
        protected HostBase(string api)
        {
             var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{ENVIRONMENT}.json", optional: true)
                .AddEnvironmentVariables(ENVIRONMENT);
                
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                       
                    builder.UseConfiguration(configurationBuilder.Build());

                    //Caso queira usar o teste em um banco de dados real, basta comentar o trecho de codigo abaixo e setar o Environment para o ambiente desejado.
                    // builder.ConfigureServices(services =>
                    // {
                    //     services.AddScoped<ILogQueryRepository, LogQueryRepositoryInMemoryMock>();

                    //     var context = services.SingleOrDefault(
                    //         d => d.ServiceType ==
                    //             typeof(DbContextOptions<LogContext>));

                    //     services.Remove(context);

                    //     services.AddDbContext<LogContext>(options =>
                    //     {
                    //         options.UseInMemoryDatabase("InMemoryDbForTesting");
                    //     });
                    // });
                });

            _client = factory.CreateClient();
            _api = api;
        }

        protected async Task<HttpResponseMessage> Get(string caminho, bool usarToken = true)
        {
            if (usarToken)
            {
               await GerarCabecalhoBearerToken();
            }

            var api = _api +"/"+ caminho;
            return await _client.GetAsync(api);
        }

        protected async Task<HttpResponseMessage> Post(string caminho, object objetoJson, bool usarToken = true)
        {
            if (usarToken)
            {
               await GerarCabecalhoBearerToken();
            }

            var api = _api + "/" + caminho;
            var json =  JsonConvert.SerializeObject(objetoJson);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PostAsync(api, content);
        }

        protected async Task<HttpResponseMessage> Put(string caminho, object objetoJson, bool usarToken = true)
        {
            if (usarToken)
            {
               await GerarCabecalhoBearerToken();
            }

            var api = _api + "/" + caminho;
            var json =  JsonConvert.SerializeObject(objetoJson);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PutAsync(api, content);
        }

        protected async Task Logar(LoginModel model)
        {
            var json =  JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/auth", content);

            response.EnsureSuccessStatusCode();

            var usuario = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            string token = usuario.token; 
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        private async Task GerarCabecalhoBearerToken()
        {
            var token = await Logar();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        private async Task<string> Logar()
        {
            await ValidarUsuarioAdmin();

            var loginModel = new LoginModel
            {
                UserName = "admin",
                Password = "admin"
            };

            var json =  JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/auth", content);

            response.EnsureSuccessStatusCode();

            var usuario = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

            return usuario.token;
        }
        
        private async Task ValidarUsuarioAdmin()
        {
            var api = "api/user/admin";
            var response = await _client.GetAsync(api);
            response.EnsureSuccessStatusCode();
        }
    }
}