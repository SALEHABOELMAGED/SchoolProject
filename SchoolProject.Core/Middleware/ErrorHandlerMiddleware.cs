using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SchoolProject.Core.Bases;
using System.Net;
using System.Text.Json;


namespace SchoolProject.Core.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        #endregion

        #region Constructors
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Actions
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Succeeded = false,
                    Message = "An unexpected error occurred.",
                    Errors = error.Message
                };

                switch (error)
                {
                    // 400 - Validation / Bad Request
                    case ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var validationErrors = e.Errors
                            .Select(err => err.PropertyName + ": " + err.ErrorMessage)
                            .ToList();
                        await response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = response.StatusCode,
                            Succeeded = false,
                            Message = "Validation failed.",
                            Errors = validationErrors
                        }));
                        break;

                    // 401 - Unauthorized
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = response.StatusCode,
                            Succeeded = false,
                            Message = "Unauthorized access.",
                            Errors = e.Message
                        }));
                        break;

                    // 403 - Forbidden
                    case InvalidOperationException e when e.Message.Contains("Forbidden"):
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        await response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = response.StatusCode,
                            Succeeded = false,
                            Message = "Access forbidden.",
                            Errors = e.Message
                        }));
                        break;

                    // 404 - Not Found
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        await response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = response.StatusCode,
                            Succeeded = false,
                            Message = "Resource not found.",
                            Errors = e.Message
                        }));
                        break;

                    // 408 - Timeout
                    case TimeoutException e:
                        response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                        await response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = response.StatusCode,
                            Succeeded = false,
                            Message = "Request timed out.",
                            Errors = e.Message
                        }));
                        break;

                    // 409 - Conflict
                    case InvalidOperationException e:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        await response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = response.StatusCode,
                            Succeeded = false,
                            Message = "Conflict occurred.",
                            Errors = e.Message
                        }));
                        break;

                    // 500 - Internal Server Error (default)
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = response.StatusCode,
                            Succeeded = false,
                            Message = "An unexpected error occurred.",
                            Errors = error.Message
                        }));
                        break;
                }
            }
        }
        #endregion
    }

    #region Extensions
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
    #endregion
}