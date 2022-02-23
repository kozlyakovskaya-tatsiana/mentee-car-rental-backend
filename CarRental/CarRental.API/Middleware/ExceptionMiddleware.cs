using System;
using System.Threading.Tasks;
using CarRental.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CarRental.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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

            var result = new ErrorDetails
            {
                StatusCode = 500,
                Title = exception.Message
            };

            switch (exception)
            {
                case NotFoundException _ :
                    result.StatusCode = 404;
                    break;
                case BadAuthorizeException _ :
                    result.StatusCode = 400;
                    break;
                case TokenExpiredException _ :
                    result.StatusCode = 401;
                    break;
                case NotVerifiedException _:
                    result.StatusCode = 401;
                    break;
            }

            context.Response.StatusCode = result.StatusCode;

            await context.Response.WriteAsync(result.ToString());
        }
    }
}
