using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CopaFilmes.BizLogic.BizRules;
using CopaFilmes.BizLogic.BizRules.Abstraction;
using CopaFilmes.BizLogic.BizRules.Helpers;
using CopaFilmes.BizLogic.BizRules.Helpers.Abstraction;
using CopaFilmes.BizLogic.BizValidations;
using CopaFilmes.BizLogic.BizValidations.Abstraction;
using CopaFilmes.BizLogic.Dtos;
using CopaFilmes.BizLogic.Facades;
using CopaFilmes.BizLogic.Facades.Abstraction;
using CopaFilmes.BizLogic.Repositories.Abstraction;
using CopaFilmes.DataAccess;
using CopaFilmes.Infrastructure.HttpClient;
using CopaFilmes.Infrastructure.HttpClient.Abstraction;
using CopaFilmes.WebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace CopaFilmes.WebApi
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builderConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json");

            Configuration = builderConfig.Build();

            services.Configure<DataAccessSettings>(Configuration.GetSection("DataAccess"));

            services.AddMvc
                    (
                        config => 
                        {
                            config.Filters.Add(typeof(CustomExceptionFilter));
                        }
                    )
                    .AddJsonOptions
                    (
                        options =>
                        {
                            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                        }
                    );

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>{
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddScoped<ICompetitionFacade, CompetitionFacade>();
            services.AddSingleton<IBizValidationFactory<CompetitionBizDto>, CompetitionBizValidationFactory>();
            services.AddScoped<IMovieRepository, MovieAzureApi>();
            services.AddScoped<ITiebreaker, TiebreakerAlphabeticalOrder>();
            services.AddSingleton<IBizRuleFactory<CompetitionBizDto>>(factory => 
            {
                return new CompetitionBizFactory
                (
                    new GroupPhase(new TiebreakerAlphabeticalOrder()),
                    new EliminatoryPhase(new TiebreakerAlphabeticalOrder()),
                    new TiebreakerAlphabeticalOrder()
                );
            });
            services.AddScoped<IHttpHandler, HttpClientHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOrigins");
            app.UseMvc();

            loggerFactory.AddFile("Log/Api_Log.txt");
            loggerFactory.AddDebug();
        }
    }
}
