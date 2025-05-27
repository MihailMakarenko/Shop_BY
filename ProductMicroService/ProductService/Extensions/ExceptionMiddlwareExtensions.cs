using Entities;
using Entities.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace ProductService.DI
{
    public static class ExceptionMiddlwareExtensions
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
                            ConflictException => StatusCodes.Status409Conflict,
                            _ => StatusCodes.Status500InternalServerError
                        };



                        if (contextFeature.Error is ValidationException validationException)
                        {
                            var errors = validationException.Errors.GroupBy(e => e.PropertyName)
                                .ToDictionary(
                                    g => g.Key,
                                    g => g.Select(e => e.ErrorMessage).ToArray()
                                );

                            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                StatusCode = context.Response.StatusCode,
                                Errors = errors
                            }));
                            return;
                        }


                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }));
                    }
                });
            });
        }
    }

}
