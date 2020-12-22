using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Meta.log;
using Meta.Domain.src;
using Meta.Controller.src;

namespace MetaAPIClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            CentralLog.LogInfo("Begin Burn");
        }
        
        public static IConfiguration StaticConfig { get; private set; }
        // public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetaGiftCardAPIClient", Version = "v1" });
            });
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetaAPIClient v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseHttpsRedirection();
            // default value
            // app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            StaticConfig = builder.Build();

            BuildAppSettingsProvider ( env.EnvironmentName );
        }
        private void BuildAppSettingsProvider( string enviropment )
        {
            CentralLog.LogInfo(String.Concat(":: Runing in :: ",enviropment));

            AppSettingsProvider.IsDevelopment = enviropment == "Development";
            AppSettingsProvider.Enviropment = enviropment;
            AppSettingsProvider.taxaJuros = StaticConfig.GetSection("Settings").GetSection("API").GetSection("taxaJuros").Value;
            


        }
    }
}
