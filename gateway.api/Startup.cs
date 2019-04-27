using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.JwtAuthorize;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.Swagger;

namespace Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelotJwtAuthorize();
            services.AddOcelot(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen();
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            var microservices = new Dictionary<string, string>(
                new KeyValuePair<string, string>[] {
                    KeyValuePair.Create("authorize", "authorize-microservice"),
                    KeyValuePair.Create("helpDeskSupport", "helpdesk-microservice")
                });
            app.UseMvc()
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                   microservices.Keys.ToList().ForEach(key =>
                   {
                       app.Map($"/{key}/swagger.json", b =>
                       {
                           b.Run(async x =>
                           {
                               var json = File.ReadAllText($"{key}.json");
                               await x.Response.WriteAsync(json);
                           });
                       });
                       options.SwaggerEndpoint($"/{key}/swagger.json", $"{microservices[key]} ");
                   });
                   options.DocumentTitle = "Record Point Demo";
               });
            await app.UseOcelot();
        }
    }
}
