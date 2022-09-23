using ApiGatewayEjemplo.Aggregator;
using ApiGatewayEjemplo.Handlers;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGatewayEjemplo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot()
                .AddDelegatingHandler<LogDelegatingHandler>(true)
                .AddSingletonDefinedAggregator<UserPostsAggregator>()
                .AddSingletonDefinedAggregator<UsersPostsAggregator>()
                .AddSingletonDefinedAggregator<ReservaDeudaAggregator>()
                .AddSingletonDefinedAggregator<CheckInVueloAggregator>()
                .AddSingletonDefinedAggregator<ReservasSinCheckInAggregator>()
                
                .AddDelegatingHandler<NoEncodingHandler>(true); 



            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiGateway", Version = "v1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiGatewayV1"));
            }
            app.UseOcelot().Wait();
        }
    }
}
