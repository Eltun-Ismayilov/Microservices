using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Microsoft.AspNetCore.Builder;
namespace APIGateway
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services
                .AddOcelot()
                .AddCacheManager(x => x.WithDictionaryHandle());

            services.AddSwaggerForOcelot(_configuration);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UsePathBase("/gateway");
                app.UseDeveloperExceptionPage();
                app.UseSwaggerForOcelotUI(_configuration, opt =>
                {
                    opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
                    opt.PathToSwaggerGenerator = "/swagger/docs";
                });
            }


            app.UseOcelot();
        }
    }
}
