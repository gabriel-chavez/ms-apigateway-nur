using ApiGatewayEjemplo.Aggregator;
using ApiGatewayEjemplo.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
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
            var allowOrigins = Configuration.GetValue<string>("AllowOrigins");
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(allowOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                      .AllowCredentials();
                });
                options.AddPolicy("AllowHeaders", builder =>
                {
                    builder.WithOrigins(allowOrigins)
                            .WithHeaders(HeaderNames.ContentType, HeaderNames.Server, HeaderNames.AccessControlAllowHeaders, HeaderNames.AccessControlExposeHeaders, "x-custom-header", "x-path", "x-record-in-use", HeaderNames.ContentDisposition);
                });
            });


            var secretKey = Configuration.GetValue<string>("JwtOptions:SecretKey");
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
            app.UseCors("CorsPolicy");
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
