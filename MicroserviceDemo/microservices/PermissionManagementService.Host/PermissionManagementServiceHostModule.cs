using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MsDemo.Shared;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement.EntityFrameworkCore;


namespace PermissionManagementService.Host
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpEventBusRabbitMqModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpAspNetCoreMultiTenancyModule)

      
        )]
    public class PermissionManagementServiceHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MsDemoConsts.IsMultiTenancyEnabled;
            });

            context.Services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.ApiName = configuration["AuthServer:ApiName"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                });

            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Permission Management Service API", Version = "v1"});
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
            });

            //Configure<AbpDistributedEntityEventOptions>(options =>
            //{
            //    options.AutoEventSelectors.Add<Permission>();
            //});

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Configuration"];
            });

            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabledForGetRequests = true;
                options.ApplicationName = "PermissionManagementService";
            });

            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services.AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "MsDemo-DataProtection-Keys");
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAbpClaimsMap();

            if (MsDemoConsts.IsMultiTenancyEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAbpRequestLocalization(); //TODO: localization?
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tenant Management Service API");
            });
            app.UseAuditing();
            app.UseConfiguredEndpoints();
        }
    }
}
