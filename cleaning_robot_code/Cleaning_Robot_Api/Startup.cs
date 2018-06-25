using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cleaning_Robot_Api.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cleaning_Robot_Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Im using Singleton becouse this service is use to read the appsetting and it allways be the same object for every request
            //which creates a single instance throughout the application. It creates the instance for the first time and reuses the same object in the all calls.
            services.AddSingleton<IConfigMessage, ConfigMsg>();
            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration, ILogger<Startup> logger)
        {
            //We use the .net core MVC routing
             app.UseMvc(ConfigureRouters);


            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(configuration["NotFound"]);
            });
        }
        /// <summary>
        /// We configure the route to handle the following
        /// Default controller: Cleanng_Robot
        /// Default action: Index
        /// In case we dont find a controller or action we send the cleaning_robot controller with the action Index
        /// </summary>
        /// <param name="routeBuilder"></param>
        private void ConfigureRouters(IRouteBuilder routeBuilder)
        {

            routeBuilder.MapRoute("Default",
                                   "{controller=cleaning_robot}/{action=Index}/");
        }
    }
}
