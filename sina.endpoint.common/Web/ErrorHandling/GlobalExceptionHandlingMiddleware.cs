using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using sina.endpoint.common.Web.Dto;

namespace sina.endpoint.common.Web.ErrorHandling
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> logger;
        private readonly IWebHostEnvironment environment;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next,
            ILogger<GlobalExceptionHandlingMiddleware> logger,
            IWebHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Uncaught exception has been detected. Trace id: {httpContext.TraceIdentifier}");
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var message = $"Something went wrong. See the logs. Trace id: {context.TraceIdentifier}";

            if (environment.IsDevelopment() || environment.IsStaging())
            {
                message += $" [{exception.Message}] ";
            }

            var responseMessageDto = new ResponseMessageDto {Message = message};

            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(new List<ResponseMessageDto> {responseMessageDto}, Formatting.Indented));
        }
    }
}