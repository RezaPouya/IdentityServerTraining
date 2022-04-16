using Tasky.Administration;
using Tasky.Administration.EntityFrameworkCore;
using Tasky.IdentityService;
using Tasky.IdentityService.EntityFrameworkCore;
using Tasky.Microservice.Shared;
using Tasky.SaaS;
using Tasky.SaaS.EntityFrameworkCore;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Tasky.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AdministrationEntityFrameworkCoreModule),
    typeof(AdministrationApplicationContractsModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(SaaSEntityFrameworkCoreModule),
    typeof(SaaSApplicationContractsModule)
)]

[DependsOn( typeof(AdministrationEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpDataModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
[DependsOn(typeof(AbpAspNetCoreMultiTenancyModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreModule))]
public class TaskyDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.Databases.Configure("SaaS", database =>
            {
                database.MappedConnections.Add("AbpTenantManagement");
                database.IsUsedByTenants = false;
            });

            options.Databases.Configure("Administration", database =>
            {
                database.MappedConnections.Add("AbpAuditLogging");
                database.MappedConnections.Add("AbpPermissionManagement");
                database.MappedConnections.Add("AbpSettingManagement");
                database.MappedConnections.Add("AbpFeatureManagement");
            });

            options.Databases.Configure("IdentityService", database =>
            {
                database.MappedConnections.Add("AbpIdentity");
                database.MappedConnections.Add("AbpIdentityServer");
            });
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("fa", "fa-IR", "فارسی ( ایران ) "));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
        });
        //Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}