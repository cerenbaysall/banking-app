using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BankingApp.AccountAPI.Data;
using BankingApp.AccountAPI.Data.IRepositories;
using BankingApp.AccountAPI.Data.Repositories;
using BankingApp.AccountAPI.Service;
using BankingApp.AccountAPI.Service.Mappers;
using BankingApp.AccountAPI.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using MediatR;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BankingApp.AccountAPI;
using Microsoft.AspNetCore.Authorization;
using BankingApp.AccountAPI.Data;

namespace BankingApp.AccountAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddDbContext<AccountDbContext>(options => options.UseInMemoryDatabase("account"));

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:SecurityKey"]))
                };
            });

            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(Configuration["Jwt:SecurityKey"]));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Account API",
                    Description = "Account API Swagger Documentation",
                    Contact = new OpenApiContact
                    {
                        Name = "Ceren Keles",
                        Email = string.Empty
                    }
                });
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountMapper, AccountMapper>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerMapper, CustomerMapper>();

            services.AddMediatR(
                typeof(CreateAccountHandler),
                typeof(GetAccountHandler),
                typeof(CreateCustomerHandler),
                typeof(GetCustomerHandler));

            services.AddLogging();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<Data.AccountDbContext>();
            var mediator = serviceProvider.GetService<IMediator>();
            
            //Data.CreateData(context, mediator);
            app.UseSwagger();
            app.UseCors("CorsPolicy");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Account API V1");
            });

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
