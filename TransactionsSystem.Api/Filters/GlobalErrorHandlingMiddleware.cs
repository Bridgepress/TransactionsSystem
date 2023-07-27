using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using TransactionsSystem.Core.Exceptions;
using ApplicationException = TransactionsSystem.Core.Exceptions.ApplicationException;

namespace TransactionsSystem.Api.Filters
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var stackTrace = exception.StackTrace;

            var exceptionResult =
                $$"""
                {
                    "message": "{{exception.Message}}",
                    "stackTrace": "{{stackTrace}}",
                    "object": {{(exception is ObjectApplicationException oae ? $"\"{JsonConvert.SerializeObject(oae.Object)}\"" : "null")}}
                }
                """;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception is ApplicationException applicationException
                ? applicationException.StatusCode
                : 500;
            _logger.Error(exception, "Unhandled exception occurred, status code is {Code}",
                context.Response.StatusCode);
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
