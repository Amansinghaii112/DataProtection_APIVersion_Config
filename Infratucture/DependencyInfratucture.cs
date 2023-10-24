using Microsoft.AspNetCore.DataProtection;
using System.Runtime.CompilerServices;

namespace DataProtection
{
    public static class DependencyInfratucture
    {
        public static IServiceCollection DependencyRegister(this IServiceCollection services, IConfiguration config)
        {
            services.AddDataProtection();
            //services.AddScoped<IDataProtectionService, DataProtectionService>();
            ServiceDescriptor dataProtectionService = new ServiceDescriptor(typeof(IDataProtectionService), service =>
            {
                var DataProtectionProviderService = service.GetRequiredService<IDataProtectionProvider>();
                string dataProtectionSecretKey = config.GetValue<string>("DataProtectionSecretKey");
                IDataProtector protector = DataProtectionProviderService.CreateProtector(dataProtectionSecretKey);
                return new DataProtectionService(protector);
            },
            ServiceLifetime.Singleton);
            services.Add(dataProtectionService);

            return services;
        }
    }
}
