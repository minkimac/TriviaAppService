using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaOrchestratorContract;
using TriviaOrchestratorImplementation;

namespace TriviaApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ITriviaOrchestrator, TriviaOrchestratorImpl>();
            services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200")));
            services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.WithOrigins("https://localhost:4200")));
            services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseCors(options => options.WithOrigins("http://localhost:4200").WithOrigins("https://localhost:4200"));
            app.UseRouting();

            // global error handler
            app.UseMiddleware<CustomErrorHandler>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
