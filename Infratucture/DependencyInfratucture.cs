using DataProtection_APIVersion_Config;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace DataProtection
{
    public static class DependencyInfratucture
    {
        public static IServiceCollection DependencyRegister(this IServiceCollection services, IConfiguration config)
        {

            var formatSettings = config.GetSection("Formatting").Get<FormatSettings>();
            formatSettings.Flag = config.GetValue<bool>("Formatting:Localize");
            var formatSettingsBind = new FormatSettings();
            config.GetSection("Formatting").Bind(formatSettingsBind);
            //services.Configure<FormatSettings>(config.GetSection("Formatting"));
           services.Configure<FormatSettings>(options => {
                options.Localize = formatSettings.Localize;
                options.Flag = formatSettings.Flag;
                options.Number = formatSettings.Number;
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                //options.ApiVersionReader = new QueryStringApiVersionReader("version");
                options.ApiVersionReader = ApiVersionReader.Combine(
                                            new HeaderApiVersionReader("api-version"),
                                            new QueryStringApiVersionReader("api-version"));
                //This will include all available versions in the API response headers.
                options.ReportApiVersions = true;
            });

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
            services.Add(dataProtectionService);

            return services;
        }
    }
}
