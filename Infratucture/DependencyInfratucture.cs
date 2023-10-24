using Microsoft.AspNetCore.DataProtection;
using System.Runtime.CompilerServices;

namespace DataProtection
{
    public static class DependencyInfratucture
    {
        public static IServiceCollection DependencyRegister(this IServiceCollection services)
        {
            services.AddDataProtection();
            //services.AddScoped<IDataProtectionService, DataProtectionService>();
            ServiceDescriptor dataProtectionService = new ServiceDescriptor(typeof(IDataProtectionService), ser =>
            {
                var DataProtectionProviderService = ser.GetRequiredService<IDataProtectionProvider>();
                var ConfigurationService = ser.GetRequiredService<IConfiguration>();
                return new DataProtectionService(DataProtectionProviderService, ConfigurationService);
            },
            ServiceLifetime.Scoped);
            services.Add(dataProtectionService);

            return services;
        }
    }
}
