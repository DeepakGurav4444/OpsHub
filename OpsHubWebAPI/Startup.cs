using AutoMapper;
using OpsHubCommonUtility.Configuration;
using OpsHubWebAPI.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.IdentityModel.Logging;
using OpsHubWebAPI.AutoMapperProfile;
using STEIWebAPI.Helper;
using OpsHubWebAPI.Helper.AuthHandlers;
using OpsHubDAL.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpsHubBAL.Firebase;
using Microsoft.Extensions.Options;
using OpsHubWebAPI.Helper.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace OpsHubWebAPI
{
    public class Startup
    {
        private AppsettingsConfig _appSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers(options=>
                options.Filters.Add<ValidateRequestFilter>()
            ).AddNewtonsoftJson();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddAutoMapper(typeof(MapperProfileDeclaration));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var serviceRegistry = new ServiceRegistry();
            services.AddMvc();
            IdentityModelEventSource.ShowPII = true;
            AppsettingsConfig appSettings = LoadConfiguration(services);
            _appSettings = appSettings;
            services.AddHostedService<FirebaseInitializerService>();
            serviceRegistry.ConfigureDataContext(services, appSettings);

            serviceRegistry.ConfigureDependencies(services, appSettings);
            services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        ValidateIssuerSigningKey = true
                    };
                }).
                AddScheme<CustomTokenAuthenticationOptions, CustomTokenAuthenticationHandler>("CustomTokenAuthenticationScheme", options => { });
;

            services.AddAuthorization(options =>
            {
                options.AddPolicy("view_all_tabs", Policies.Create("view_all_tabs"));
                options.AddPolicy("create_entry", Policies.Create("create_entry"));
                options.AddPolicy("approve_entry", Policies.Create("approve_entry"));
                options.AddPolicy("final_approve", Policies.Create("final_approve"));
                options.AddPolicy("create_user", Policies.Create("create_user"));
                options.AddPolicy("create_tenant", Policies.Create("create_tenant"));
                options.AddPolicy("create_admin", Policies.Create("create_admin"));
                options.AddPolicy("subscribe_plan", Policies.Create("subscribe_plan"));
            });
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("AuthToken", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Bearer {JWT token}"
                });
                c.AddSecurityDefinition("CustomToken", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Custom {Custom token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "AuthToken"
                            }
                        },
                        Array.Empty<string>() // No scopes required
                    },
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "CustomToken"
                            }
                        },
                        Array.Empty<string>() // No scopes required
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            var envr = Configuration["AgriBidinData:Environment"];


            app.Use(async (context, next) =>
            {
                var url = context.Request.GetTypedHeaders().Referer;
                if (url != null)
                {
                    if (!url.ToString().StartsWith("https") && envr != "DEV")
                    {
                        byte[] data = Encoding.ASCII.GetBytes("not recognized request");
                        context.Response.StatusCode = 403;
                        await context.Response.Body.WriteAsync(data);
                        return;
                    }
                    await next();
                }
                else
                {
                    await next();
                }

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("My Running Enviroment:" + env.EnvironmentName + "");
                });
            });
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "OpsHubAPI");
            });
        }

        private AppsettingsConfig LoadConfiguration(IServiceCollection services)
        {
            AppsettingsConfig appSettings = new AppsettingsConfig();
            Configuration.Bind(appSettings);
            services.AddSingleton(appSettings);
            return appSettings;
        }

    }
}
