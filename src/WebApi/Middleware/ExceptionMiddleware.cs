using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest, "Internal Error");
            }
            catch (ValidationException ex) // Ejemplo de excepciones de validación
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status422UnprocessableEntity, "ValidacionFallida");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError, "ErrorInterno");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, string errorCode)
        {
            var response = new
            {
                mensaje = exception.Message,
                codigo = errorCode,
                estado = statusCode
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
