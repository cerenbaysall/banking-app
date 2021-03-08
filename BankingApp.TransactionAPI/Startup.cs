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
using BankingApp.TransactionAPI.Data;
using BankingApp.TransactionAPI.Data.IRepositories;
using BankingApp.TransactionAPI.Data.Repositories;
using BankingApp.TransactionAPI.Service;
using BankingApp.TransactionAPI.Service.Mappers;
using BankingApp.TransactionAPI.Service.Services;
using BankingApp.TransactionAPI.EventListener;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using MediatR;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BankingApp.TransactionAPI;
using Microsoft.AspNetCore.Authorization;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BankingApp.TransactionAPI
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
            services.AddDbContext<TransactionDbContext>(options => options.UseInMemoryDatabase("account"));

            var consumerConfig = new ConsumerConfig();  
            Configuration.Bind("consumer", consumerConfig);  
            services.AddSingleton<ConsumerConfig>(consumerConfig);
            
            services.AddHostedService(serviceProvider => new MoneyTransferEventListener(new MoneyTransferEventConfig
            {
                dContext = services.BuildServiceProvider().GetService<TransactionDbContext>(),
                logger = services.BuildServiceProvider().GetService<ILogger<MoneyTransferEventListener>>()
            }));


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
                    Title = "Transaction API",
                    Description = "Transaction API Swagger Documentation",
                    Contact = new OpenApiContact
                    {
                        Name = "Ceren Keles",
                        Email = string.Empty
                    }
                });
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionMapper, TransactionMapper>();

            services.AddMediatR(
                typeof(CreateTransactionHandler),
                typeof(GetTransactionHandler),
                typeof(GetAccountTransactionsHandler));

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
            var context = serviceProvider.GetService<Data.TransactionDbContext>();
            var mediator = serviceProvider.GetService<IMediator>();
            
            app.UseSwagger();
            app.UseCors("CorsPolicy");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transaction API V1");
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
