using Newtonsoft.Json;

namespace DataProtection_APIVersion_Config.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse();

                if (ex.GetType() == typeof(InvalidDataException)) 
                {
                    errorResponse.ExceptionType = ex.GetType().ToString();
                    errorResponse.Message = ex.Message;
                }

                if (ex.GetType() == typeof(System.Security.Cryptography.CryptographicException))
                {
                    errorResponse.ExceptionType = ex.GetType().ToString();
                    errorResponse.Message = ex.Message;
                }
                else
                {
                    errorResponse.ExceptionType = ex.GetType().ToString();
                    errorResponse.Message = "Internal Server Error";
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var jsonResponse = JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
