using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using qsLibPack.Middlewares;
using qsLibPack.Validations.IoC;
using qsLog.Applications.IoC;
using qsLog.Infrastructure.Database.MongoDB.IoC;
using qsLog.Infrastructure.Database.MySql.EF.IoC;
using qsLogPack.IoC;
using Rebus.ServiceProvider;
using qsLog.Infrastructure.RabbitMQ.Services;
using qsLog.Infrastructure.RabbitMQ.IoC;
using qsLog.Api.Services;
using MassTransit;
using qsLog.Api.Consumers;

namespace qsLog
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(); // Inclui o servico de aplicacao.
           
            // services.AddInfraDatabaseMySql(Configuration.GetConnectionString("MySqlConn")); //Inclui o banco de dados da infra
            services.AddInfraDatabaseMongoDB(Configuration);
            services.AddValidationService(); //Adicionado a validacao com mensagens do qsLibPack
            // services.AddQsLog(Configuration); //Adicionando os logs
            
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "qsLog", Version = "v1" });
            });

            // if (!Env.IsStaging())
            // {
            //     services.AddQsRabbitMQ(Configuration);
            //     services.AddHostedService<ConsumerService>();
            // }

            if (!Env.IsStaging())
            {
                services.AddMassTransit(cfg =>
                {
                    cfg.AddConsumer<LogConsumer>();
                    cfg.UsingRabbitMq((context, config) =>
                    {
                        config.ReceiveEndpoint("qslog", e =>
                        {
                            e.ClearMessageDeserializers();
                            e.UseRawJsonSerializer();
                            e.ConfigureConsumer<LogConsumer>(context);
                        });

                        config.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                    });
                });

                services.AddMassTransitHostedService();
            }


            
            this.ConfigureJWT(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "qsLog v1"));
            }

            app.UseRouting();

             app.UseCors(b => 
                        b.AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowAnyOrigin()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseValidationService(); //Usando a validacao de mensagens do qsLibPack

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureJWT(IServiceCollection services)
        {
            var securityKey = Configuration.GetSection("SecurityKey").Value;

            var key = Encoding.ASCII.GetBytes(securityKey);
            services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
