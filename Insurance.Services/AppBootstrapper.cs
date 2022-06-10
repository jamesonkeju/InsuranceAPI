using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Insurance.Data;
using Insurance.Services.AuditLog.Concrete;
using Insurance.Services.CommonRoute;
using Insurance.Services.CommonRoute.concrete;
using Insurance.Services.Country;
using Insurance.Services.Country.Concrete;
using Insurance.Services.Permission;
using Insurance.Services.Permission.Concrete;
using Insurance.Services.Permission.Interface;
using Insurance.Services.Role.Concrete;
using Insurance.Services.Role.Interface;
using Insurance.Services.SystemSetting.Concrete;
using Insurance.Services.SystemSetting.Interface;
using Insurance.Services.User.Interface;
using Insurance.Services.User.Concrete;
using Insurance.Services.FAQs.Interface;
using Insurance.Services.FAQs.Concrete;
using Insurance.Services.FileHandler;
using Insurance.Services.Report.Interface;
using Insurance.Services.Report.Concete;
using Insurance.Services.AxaMansard.Interface;
using Insurance.Services.AxaMansard.Concrete;

namespace Insurance.Services
{
    public class AppBootstrapper
    {
        private static void AutoInjectLayers(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromCallingAssembly().AddClasses(classes => classes
                    .Where(type => type.Name.EndsWith("Repository") || type.Name.EndsWith("Service")), false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        }
        public static void InitServices(IServiceCollection services)
        {
            AutoInjectLayers(services);


            
            services.AddScoped<DbContext, InsuranceAppContext>();
            services.AddTransient(typeof(DbContextOptions<InsuranceAppContext>));
            

            services.AddTransient<IActivityLog, ActivityLogServices>(); 
            services.AddTransient<IActivityLog, ActivityLogServices>();
            services.AddTransient<ICommonRoute, CommonRouteServices>();
            services.AddTransient<IAxamansardInsurance, AxamansardService>();
        }



    }
}
