using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlackBot.Api.Factories;
using SlackBot.Api.Services;
using SlackBot.Domain;
using SlackBot.Domain.Contracts.Factories;
using SlackBot.Domain.Contracts.Services;

namespace SlackBot.Api
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
            services.AddScoped<IQueryStringFactory, QueryStringFactory>();
            services.AddScoped<IRestClient, RestClient>();
            services.Configure<SlackConfiguration>(Configuration.GetSection(nameof(SlackConfiguration)));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
