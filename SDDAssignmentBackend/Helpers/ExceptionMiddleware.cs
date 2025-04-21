using System;
using System.Net;

namespace SDDAssignmentBackend.Helpers
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                var test = 0;
            }
            catch (LogicException ex)
            {
                _logger.LogError($"Logic Expcepion: {ex}");
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Logic Expcepion: {ex}"); 
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsJsonAsync(new { Message = exception.Message });
        }
    }
}
