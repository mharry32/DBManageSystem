using Autofac;
using Autofac.Extensions.DependencyInjection;
using DBManageSystem.Core;
using DBManageSystem.Core.Constants;
using DBManageSystem.Core.Entities;
using DBManageSystem.Infrastructure;
using DBManageSystem.Infrastructure.Configs;
using DBManageSystem.Infrastructure.Data;
using DBManageSystem.Infrastructure.Identity;
using DBManageSystem.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;
using DBManageSystem.ManageWebAPI.Endpoints.AuthEndpoints;

namespace DBManageSystem.ManageWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Logging.AddConsole();

            builder.Services.AddDbContext<DbManageSysDbContext>(op =>
            {
                op.UseMySql(builder.Configuration["DbManageSysDbConnectString"], MySqlServerVersion.LatestSupportedServerVersion);
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(op =>
            {
            op.UseMySql(builder.Configuration["AppIdentityDbConnectString"], MySqlServerVersion.LatestSupportedServerVersion);
            });
            builder.Services.AddIdentity<User, Role>()
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();

            var jwtsecret = builder.Configuration[JwtSecret.Name];
            builder.Services.AddSingleton<JwtSecret>(new JwtSecret(jwtsecret));

            var defaultPassword = builder.Configuration[DefaultPassword.Name];
            builder.Services.AddSingleton<DefaultPassword>(new DefaultPassword(defaultPassword));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(DbManageSysRepository<>));
            builder.Services.AddScoped(typeof(IReadRepository<>), typeof(DbManageSysRepository<>));
            builder.Services.AddAutoMapper(typeof(Login));
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DBManageSystem API", Version = "v1" });
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
            });
            });


            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new DefaultCoreModule());
                containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
            });
            // Add services to the container.

            
            var key = Encoding.ASCII.GetBytes(jwtsecret);
            builder.Services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                config.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthorization();



            using (var scope = app.Services.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;
                try
                {
                    var userManager = scopedProvider.GetRequiredService<UserManager<User>>();
                    var roleManager = scopedProvider.GetRequiredService<RoleManager<Role>>();
                    var identityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
                    var seedTask =  AppIdentityDbContextSeed.SeedAsync(identityContext, userManager, roleManager,new DefaultPassword(defaultPassword));
                    seedTask.Wait();
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }


            app.MapControllers();

            app.Run();
        }
    }
}