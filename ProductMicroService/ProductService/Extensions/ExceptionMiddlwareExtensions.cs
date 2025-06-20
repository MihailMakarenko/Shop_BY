using Entities;
using Entities.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace ProductService.DI
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            ValidationException => StatusCodes.Status422UnprocessableEntity,
                            ValidationAppException => StatusCodes.Status400BadRequest,
                            ConflictException => StatusCodes.Status409Conflict,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        switch (contextFeature.Error)
                        {
                            case ValidationException validationException:
                                var errors = validationException.Errors
                                    .GroupBy(e => e.PropertyName)
                                    .ToDictionary(
                                        g => g.Key,
                                        g => g.Select(e => e.ErrorMessage).ToArray()
                                    );
                                await WriteErrorResponse(context, errors);
                                break;

                            case ValidationAppException validationAppException:
                                await WriteErrorResponse(context, validationAppException.Errors);
                                break;

                            default:
                                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                                {
                                    StatusCode = context.Response.StatusCode,
                                    Message = contextFeature.Error.Message
                                }));
                                break;
                        }
                    }
                });
            });
        }

        private static async Task WriteErrorResponse(HttpContext context, Dictionary<string, string[]> errors)
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Errors = errors
            }));
        }

        private static async Task WriteErrorResponse(HttpContext context, IDictionary<string, string[]> errors)
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Errors = errors
            }));
        }
    }
}