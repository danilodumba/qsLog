using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using qsLog.Infrastructure.Database.MySql.EF.Contexts;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace qsLog.Test.Integration
{
    public abstract class HostBase
    {
        private readonly HttpClient _client;
        private readonly string _api;
        const string ENVIRONMENT = "Development";
        protected HostBase(string api)
        {
            //  var configurationBuilder = new ConfigurationBuilder()
            //     .AddJsonFile($"appsettings.{ENVIRONMENT}.json", optional: true)
            //     .AddEnvironmentVariables();
                
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                //    builder.UseConfiguration(configurationBuilder.Build());

                    builder.ConfigureServices(services =>
                    {
                        var context = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<LogContext>));

                        services.Remove(context);

                        services.AddDbContext<LogContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });
                    });
                });

            _client = factory.CreateClient();
            _api = api;
        }

        protected async Task<HttpResponseMessage> Get(string path)
        {
            var api = _api +"/"+ path;
            return await _client.GetAsync(api);
        }

        protected async Task<HttpResponseMessage> Post(string path, object objectJson)
        {
            var api = _api + "/" + path;
            var json =  JsonConvert.SerializeObject(objectJson);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PostAsync(api, content);
        }

        protected async Task<HttpResponseMessage> Put(string path, object objectJson)
        {
            var api = _api + "/" + path;
            var json =  JsonConvert.SerializeObject(objectJson);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PutAsync(api, content);
        }
    }
}