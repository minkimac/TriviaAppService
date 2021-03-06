using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Trivia Quiz API";
                    document.Info.Description = "API to perform operations on trivia data";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Mayank Sharma",
                        Email = "minkimac.work@gmail.com",
                        Url = "https://twitter.com/minkimac"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseCors(options => options.AllowAnyOrigin());
            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();

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
