using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            _next = next;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// RequestDelegate can’t process requests without it
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handels all exception along the way
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "appication/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //create response with parametres depending on our host configuration
            var response = _hostEnvironment.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, exception.Message, exception.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode, "Internal Server Error");

            SetSeverityException(ref response);

            //set naming policy
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }

        private void SetSeverityException(ref ApiException apiException)
        {
            switch (apiException.StatusCode)
            {
                case 500:
                    apiException.Severity = Severity.Error;
                    break;
            }
        }
    }
}
