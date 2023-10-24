using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace DataProtection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddApiVersioning(options =>
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

            builder.Services.AddControllers();
            builder.Services.DependencyRegister(builder.Configuration);

            //"This operates according to the Last-In-First-Out (LIFO) pattern,
            //which means that if duplicate keys exist in multiple configuration files,
            //the file added last in the hierarchy will take precedence and overwrite the keys from other files." 
            builder.Configuration.AddJsonFile("DataProtectionConfig.json", 
                /*Optional parameter, which means that the specified file may or may not be available */false,
                /*ReloadOnChange parameter, enables the updated data to be reflected when changes are applied to the file.*/true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}