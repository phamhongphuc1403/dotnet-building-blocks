using System.Collections.Concurrent;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Infrastructure.Identity.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.Presentation.API.Middlewares;

public static class CustomExceptionHandlerMiddleware
{
    private static readonly ConcurrentDictionary<Type, ExceptionMapping> CustomExceptionDictionary = new();

    static CustomExceptionHandlerMiddleware()
    {
        AddCustomExceptionResponse<EntityNotFoundException>(HttpStatusCode.NotFound);
        AddCustomExceptionResponse<NotImplementedException>(HttpStatusCode.NotImplemented);
        AddCustomExceptionResponse<EntityConflictException>(HttpStatusCode.Conflict);
        AddCustomExceptionResponse<ValidationException>(HttpStatusCode.BadRequest);
        AddCustomExceptionResponse<AuthenticationException>(HttpStatusCode.Unauthorized);
        AddCustomExceptionResponse<FluentValidation.ValidationException>(HttpStatusCode.BadRequest,
            "Validation error",
            ex => ex.Errors.Select(x =>
                new { x.PropertyName, x.ErrorMessage }));
        AddCustomExceptionResponse<UnauthorizedException>(HttpStatusCode.Forbidden);
        AddCustomExceptionResponse<IdentityException>(HttpStatusCode.BadRequest, "Identity error",
            ex => ex.Errors.Select(x => new { x.Code, x.Description }));
    }

    public static void AddCustomExceptionResponse<TException>(
        HttpStatusCode statusCode,
        string? message = null,
        Func<TException, object>? customResponseFactory = null)
        where TException : Exception
    {
        CustomExceptionDictionary[typeof(TException)] = new ExceptionMapping
        {
            StatusCode = (int)statusCode,
            Message = message,
            CustomResponseFactory = customResponseFactory != null
                ? ex => customResponseFactory((TException)ex)
                : null
        };
    }

    public static IApplicationBuilder UseHttpExceptionHandler(this IApplicationBuilder app, IHostEnvironment env)
    {
        return app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(
                    GetResponseBody(exception, context)));
            });
        });
    }

    public static ProblemDetails GetResponseBody(Exception? exception, HttpContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred on the server.",
            Status = 500,
            Detail = exception?.StackTrace
        };
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        problemDetails.Extensions.Add("errors", exception?.Message ?? null);

        if (exception == null) return problemDetails;

        var exceptionType = exception.GetType();

        if (CustomExceptionDictionary.TryGetValue(exceptionType, out var mapping))
        {
            problemDetails = mapping.ToProblemDetails(exception, context);

            return problemDetails;
        }

        foreach (var entry in CustomExceptionDictionary)
            if (entry.Key.IsAssignableFrom(exceptionType))
            {
                problemDetails = entry.Value.ToProblemDetails(exception, context);

                return problemDetails;
            }

        return problemDetails;
    }
}

public class ExceptionMapping
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public Func<Exception, object>? CustomResponseFactory { get; set; }

    public ProblemDetails ToProblemDetails(Exception exception, HttpContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Title = Message ?? exception.Message,
            Status = StatusCode,
            Detail = exception.StackTrace
        };

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var errors = CustomResponseFactory?.Invoke(exception);

        var serializedErrors = JsonSerializer.Serialize(errors, jsonOptions);
        var deserializedErrors = JsonSerializer.Deserialize<object>(serializedErrors, jsonOptions);
        problemDetails.Extensions.Add("errors", deserializedErrors);
        return problemDetails;
    }
}