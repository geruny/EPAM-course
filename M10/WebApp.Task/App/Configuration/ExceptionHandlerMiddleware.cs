using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace App.Configuration
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        public Task HandleException(HttpContext httpContext, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case KeyNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case ArgumentException:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = exception.Message });

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) code;

            return httpContext.Response.WriteAsync(result);
        }
    }
}