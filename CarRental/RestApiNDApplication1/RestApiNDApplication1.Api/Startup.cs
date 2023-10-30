﻿using AutoMapper;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using RestApiNDApplication1.Api;
using RestApiNDApplication1.Domain.Mapping;
using RestApiNDApplication1.Domain.Service;
using RestApiNDApplication1.Entity.Context;
using RestApiNDApplication1.Entity.Migrations;
using RestApiNDApplication1.Entity.Queries;
using RestApiNDApplication1.Entity.Repository;
using RestApiNDApplication1.Entity.UnitofWork;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Designed by AnaSoft Inc. 2019
/// http://www.anasoft.net/apincore 
///
/// NOTE:
/// Must update database connection in appsettings.json - "RestApiNDApplication1.ApiDB"
/// </summary>

namespace RestApiNDApplication1.Api;
public partial class Startup
{

    public static IConfiguration Configuration { get; set; }
    public IWebHostEnvironment HostingEnvironment { get; private set; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        HostingEnvironment = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        Log.Information("Startup::ConfigureServices");

        try
        {
            //db service
            //provides connection string for RestApiNDApplication1Context with Dapper implementation
            services.AddTransient<RestApiNDApplication1Context, RestApiNDApplication1Context>(sp =>
            {
                return new RestApiNDApplication1Context(Configuration["ConnectionStrings:RestApiNDApplication1DB"]);
            });

            //fluent migration services
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                // Set the connection string
                .WithGlobalConnectionString(Configuration["ConnectionStrings:RestApiNDApplication1DBMigration"])
                //.AddSqlServer()
                .AddSqlServer2012()
                //.AddSqlServer2016()
                // Define the assemblies containing the migrations
                .ScanIn(typeof(Migration_Initial).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole().AddSerilog());



            services.AddControllers(
            opt =>
            {
                //Custom filters can be added here 
                //opt.Filters.Add(typeof(CustomFilterAttribute));
                //opt.Filters.Add(new ProducesAttribute("application/json"));
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            #region "API versioning"
            //API versioning service
            services.AddApiVersioning(
                o =>
                {
                    //o.Conventions.Controller<UserController>().HasApiVersion(1, 0);
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.ReportApiVersions = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.ApiVersionReader = new UrlSegmentApiVersionReader();
                }
                );

            // format code as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                //versioning by url segment
                options.SubstituteApiVersionInUrl = true;
            });
            #endregion

            #region "Authentication"
            //JWT API authentication service
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            }
            );
            #endregion

            #region "CORS"
            // include support for CORS
            // More often than not, we will want to specify that our API accepts requests coming from other origins (other domains). When issuing AJAX requests, browsers make preflights to check if a server accepts requests from the domain hosting the web app. If the response for these preflights don't contain at least the Access-Control-Allow-Origin header specifying that accepts requests from the original domain, browsers won't proceed with the real requests (to improve security).
            services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy-public",
                        builder => builder.AllowAnyOrigin()   //WithOrigins and define a specific origin to be allowed (e.g. https://mydomain.com)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                    //.AllowCredentials()
                    .Build());
                });
            #endregion

            #region "MVC and JSON options"
            services.AddMvc(option => option.EnableEndpointRouting = false)
                     .AddNewtonsoftJson();
            #endregion

            #region "DI code"
            //general unitofwork injections
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //services injections
            services.AddTransient(typeof(AccountService<,>), typeof(AccountService<,>));
            services.AddTransient(typeof(UserService<,>), typeof(UserService<,>));
            services.AddTransient(typeof(AccountServiceAsync<,>), typeof(AccountServiceAsync<,>));
            services.AddTransient(typeof(UserServiceAsync<,>), typeof(UserServiceAsync<,>));
            //
            SetAdditionalDIServices(services);
            //...add other services
            //
            services.AddTransient(typeof(IService<,>), typeof(GenericService<,>));
            services.AddTransient(typeof(IServiceAsync<,>), typeof(GenericServiceAsync<,>));
            #endregion

            //data mapper services configuration
            services.AddAutoMapper(typeof(MappingProfile));


        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
        }
    }


    //call scaffolded class method to add DIs
    partial void SetAdditionalDIServices(IServiceCollection services);


    // This method gets called by the runtime
    // This method can be used to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        Log.Information("Startup::Configure");

        try
        {

            //Forwarded Headers Middleware should run before other middleware. This ordering ensures that the middleware relying on forwarded headers information can consume the header values for processing.
            app.UseForwardedHeaders();

            if (env.EnvironmentName == "Development")
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseMiddleware<ExceptionHandler>();
            }

            app.UseCors("CorsPolicy-public");  //apply to every request
            app.UseAuthentication(); //needs to be up in the pipeline, before MVC
            app.UseAuthorization();

            app.UseMvc();


            //migrations and seeds from json files
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (Configuration["ConnectionStrings:UseMigrationService"] == "True")
                {
                    var runner = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                    DatabaseHelper.CreateIfNotExists(Configuration["ConnectionStrings:RestApiNDApplication1DB"]);  //Create database process must be outside migration transaction
                    runner.MigrateUp();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
        }
    }
}









