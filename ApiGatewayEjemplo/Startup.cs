using ApiGatewayEjemplo.Aggregator;
using ApiGatewayEjemplo.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

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
            var secretKey = Configuration.GetValue<string>("JwtOptions:SecretKey");
            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                        ClockSkew = new System.TimeSpan(0)
                    };
                });
                


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
            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
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
