using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnvSafe.Example.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariablesSafe();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        { }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            => app.Run(async (context)
                => await context.Response.WriteAsync("All environment variables are ok!"));
    }
}