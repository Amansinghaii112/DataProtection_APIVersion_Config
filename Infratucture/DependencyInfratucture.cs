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
            services.Configure<FormatSettings>(options =>
            {
                options.Localize = formatSettings.Localize;
                options.Flag = formatSettings.Flag;
                options.Number = formatSettings.Number;
            });

            // Add the DI for HttpContext.
            services.AddHttpContextAccessor();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                //options.ApiVersionReader = new QueryStringApiVersionReader("version");
                options.ApiVersionReader = ApiVersionReader.Combine(
                    //http://localhost:5086/api/EncryptionDecryption/Encrypt/Singhai?api-version=2.0
                                            new HeaderApiVersionReader("api-version"),
                                            //api-version 2.0 -- Add these key value pair in the request header.
                                            new QueryStringApiVersionReader("api-version"));
                //This will include all available versions in the API response headers.
                options.ReportApiVersions = true;
            });

            services.AddDataProtection();
            //services.AddSingleton<IDataProtectionService>
            //    (service =>
            //    {
            //        return new DataProtectionService(service.GetRequiredService<IDataProtectionProvider>().CreateProtector(config.GetValue<string>("DataProtectionSecretKey")));
            //    });

            ServiceDescriptor dataProtectionService = new ServiceDescriptor(typeof(IDataProtectionService), service =>
            {
                IDataProtectionProvider dataProtectionProviderService = service.GetRequiredService<IDataProtectionProvider>();
                string dataProtectionSecretKey = config.GetValue<string>("DataProtectionSecretKey");
                IDataProtector protector = dataProtectionProviderService.CreateProtector(dataProtectionSecretKey);
                return new DataProtectionService(protector);
            },
            ServiceLifetime.Singleton);
            services.Add(dataProtectionService);

            return services;
        }
    }
}
