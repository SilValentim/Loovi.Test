using System.Net;
using System.Text.Json;
using FluentValidation;
using Loovi.Test.Common.Responses;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Loovi.Test.WebApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(e => new ValidationError
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }).ToList();

                var response = ApiResponse<object>.Fail("Validation failed", errors);

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));

            }
            catch(KeyNotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";

                var response = ApiResponse<object>.Fail(ex.Message);

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = ApiResponse<object>.Fail("An unexpected error occurred");

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
 

        }
    }
}
