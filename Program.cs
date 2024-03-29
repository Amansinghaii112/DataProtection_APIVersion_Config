using DataProtection_APIVersion_Config.Middleware;

namespace DataProtection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}