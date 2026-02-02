using Newtonsoft.Json;
using System.Net;

namespace Glorri.API.CustomMiddlewares
{
    public class GlobalExceptionHandler
    {
        readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ((int)HttpStatusCode.InternalServerError);
            context.Response.ContentType = "application/json";
            var response = new
            {
                context.Response.StatusCode,
                ex.Message
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
