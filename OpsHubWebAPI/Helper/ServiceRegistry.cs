using OpsHubCommonUtility.Configuration;
using OpsHubCommonUtility.Logger;
using OpsHubDAL.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using OpsHubDTOModel.Paging;
using OpsHubDAL.SubPlan;
using OpsHubBAL.SubPlan;
using OpsHubDAL.EmailRepo;
using OpsHubBAL.Tenant;
using OpsHubDAL.TenantRepo;
using OpsHubDAL.TenModRepo;
using OpsHubBAL.SubPlanModMap;
using OpsHubDAL.TenSubMapRepo;
using OpsHubBAL.TenantUser;
using OpsHubDAL.UserRoleMapRepo;
using OpsHubDAL.UserRolePerRepo;
using OpsHubDAL.TenUserRepo;
using OpsHubBAL.Login;
using OpsHubDAL.JWTService;
using OpsHubDAL.SysAdminRepo;
using Microsoft.AspNetCore.Authorization;
using OpsHubWebAPI.Helper.AuthHandlers;
using OpsHubDAL.ClientRepo;
using OpsHubBAL.Client;
using OpsHubDAL.NotificationRepo;
using OpsHubDAL.WfStepRepo;
using OpsHubDAL.UserRoleRepo;
using OpsHubDAL.ClientWfRepo;


namespace OpsHubWebAPI.Helper
{
    public class ServiceRegistry
    {
        public void ConfigureDependencies(IServiceCollection services, AppsettingsConfig appSettings)
        {
            #region Buiseness Access Layer
            services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<ITenantUserService, TenantUserService>();
            services.AddScoped<ILoginService,LoginService>();
            services.AddScoped<IClientService,ClientService>();
            #endregion

            #region Data Access Layer
            services.AddScoped<ISubScriptionPlanRepository, SubScriptionPlanRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ITenantUserRepository, TenantUserRepository>();
            services.AddScoped<ITenantModuleRepository, TenantModuleRepository>();
            services.AddScoped<ITenantSubMapRepository,TenantSubMapRepository>();
            services.AddScoped<ISubPlanModMapRepository, SubPlanModMapRepository>();
            services.AddScoped<IUserRolMapRepository, UserRolMapRepository>();
            services.AddScoped<IUserRolePerRepository,UserRolePerRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<ISysAdminRepository,SysAdminRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<INotificationRepository,NotificationRepository>();
            services.AddScoped<IUserRolMapRepository, UserRolMapRepository>();
            services.AddScoped<IWfStepRepository, WfStepRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IClientWfRepository, ClientWfRepository>();
            #endregion

            #region Common Layer
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IPagingParameter, PagingParameter>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            #endregion
        }
        public void ConfigureDataContext(IServiceCollection services, AppsettingsConfig appSettings)
        {
            var connString = appSettings.OpsHubData.ConnectToDb.ConnectionString;
            //var loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    builder
            //        .AddConsole()
            //        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            //});
            services.AddDbContextPool<IOpsHubContext, OpsHubContext>(options =>
            {
                options.UseMySql(connString, new MySqlServerVersion(new Version(8, 0, 37))
                    //mySqlOptions => {
                    //    mySqlOptions.EnableRetryOnFailure(3);
                    //    mySqlOptions.CommandTimeout(60);
                    //}
                    )
                /*.EnableSensitiveDataLogging().UseLoggerFactory(loggerFactory)*/;
            },
            200);
        }
    }
}
