using RentalCar.API.Commom;
using RentalCar.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace RentalCar.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next) => _next = next;
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                await HandleAsync(context, HttpStatusCode.BadRequest, ex.Message, ex.ToString());
            }
            catch(Exception ex)
            {
                await HandleAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.", ex.ToString());
            }
        }
        private static async Task HandleAsync(HttpContext context, HttpStatusCode statusCode, string message, string trace)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var error = new ErrorDetails { StatusCode = (int)statusCode, Message = message, Trace = trace };
            var json = JsonSerializer.Serialize(error);
            await context.Response.WriteAsync(json);
        }
    }
}
