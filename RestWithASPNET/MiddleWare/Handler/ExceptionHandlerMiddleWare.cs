using RestWithASPNET.Exceptions;
using System.Net;
using System.Text.Json;

namespace RestWithASPNET.MiddleWare.Handler
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleWare> _logger;
        public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            switch (exception)
            {
                case ResourceNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case RequiredObjectIsNullException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                    break;
            }
        
            var errorResponse = new
            {
                message = exception.Message,
                statusCode = response.StatusCode
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
