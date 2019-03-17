using System;
using AutoMapper;
using BusinessLayer;
using Common;
using CommonHttp;
using CommonHttp.Interfaces;
using CommonHttp.Serializers;
using CommonHttp.Serializers.Interfaces;
using DataLayer;
using DataLayer.Interfaces;
using DataLayer.Interfaces.Repositories;
using DataLayer.Repositories;
using Identity.Controllers;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace PostsCommentsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration["ElasticSearchHost"]))
                {
                    AutoRegisterTemplate = true
                })
                .CreateLogger();


            // Uncomment if Serilog is not working
            // Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOData();
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["RedisHost"];
                options.InstanceName = "master";
            });

            services.AddDbContext<BaseContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
            RegisterDependencies(services);
            RegisterDependenciesForWebServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
            loggerFactory.AddSerilog();
            app.UseHttpsRedirection();
            app.UseMvc(b =>
            {
                b.Count().Filter().OrderBy().Expand().Select().MaxTop(100);
                b.EnableDependencyInjection();
            });

            app.UpdateDatabase();
        }

        private static void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
        }

        private static void RegisterDependenciesForWebServices(IServiceCollection services)
        {
            services.AddTransient<IContentSerializer, JsonSerializer>();
            services.AddTransient<IContentSerializer, TextPlainSerializer>();
            services.AddTransient<IContentSerializerFactory, ContentSerializerFactory>();
            services.AddTransient<IHttpClientRest, HttpClientRest>();
            services
                .AddTransient<IGeneratorWebServiceProxy<IIdentityContract>, GeneratorWebServiceProxy<IIdentityContract>
                >();
        }
    }
}