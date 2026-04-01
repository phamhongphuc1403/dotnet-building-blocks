using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using BuildingBlock.Infrastructure.Identity.Exceptions;
using BuildingBlock.Presentation.API.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests.Presentations.Middlewares;

public class CustomExceptionHandlerMiddlewareTest
{
    [Fact]
    public async Task WriteResponseJson_NullException_ReturnsExpectedJson()
    {
        // Arrange
        var context = CreateHttpContext(null);

        // Act
        await InvokeMiddlewareDelegate(context);

        // Assert
        var responseJson = await ReadResponseJson(context);
        var jsonDocument = JsonDocument.Parse(responseJson);

        Assert.Equal(500, jsonDocument.RootElement.GetProperty("status").GetInt32());
        Assert.Equal("An error occurred on the server.", jsonDocument.RootElement.GetProperty("title").GetString());
        Assert.False(jsonDocument.RootElement.TryGetProperty("detail", out _));
        Assert.Equal("test-trace-id", jsonDocument.RootElement.GetProperty("traceId").GetString());
        Assert.True(jsonDocument.RootElement.TryGetProperty("errors", out var errors));
        Assert.True(errors.ValueKind == JsonValueKind.Null);
    }

    [Fact]
    public async Task WriteResponseJson_AuthenticationException_ReturnsExpectedJson()
    {
        // Arrange
        var exception = new AuthenticationException("Authentication failed");

        SetMockStackTrace(exception);

        var context = CreateHttpContext(exception);

        // Act
        await InvokeMiddlewareDelegate(context);

        // Assert
        var responseJson = await ReadResponseJson(context);
        var jsonDocument = JsonDocument.Parse(responseJson);

        Assert.Equal(401, jsonDocument.RootElement.GetProperty("status").GetInt32());
        Assert.Equal("Authentication failed", jsonDocument.RootElement.GetProperty("title").GetString());
        Assert.Equal(exception.StackTrace, jsonDocument.RootElement.GetProperty("detail").GetString());
        Assert.Equal("test-trace-id", jsonDocument.RootElement.GetProperty("traceId").GetString());
        Assert.True(jsonDocument.RootElement.TryGetProperty("errors", out var errors));
        Assert.True(errors.ValueKind == JsonValueKind.Null);
    }

    [Fact]
    public async Task WriteResponseJson_IdentityException_ReturnsExpectedJson()
    {
        // Arrange
        var identityErrors = new[]
        {
            new IdentityError { Code = "DuplicateEmail", Description = "Email is already taken" },
            new IdentityError { Code = "PasswordTooShort", Description = "Password must be at least 6 characters" }
        };
        var exception = new IdentityException(identityErrors);

        SetMockStackTrace(exception);

        var context = CreateHttpContext(exception);

        // Act
        await InvokeMiddlewareDelegate(context);

        // Assert
        var responseJson = await ReadResponseJson(context);
        var jsonDocument = JsonDocument.Parse(responseJson);

        Assert.Equal(400, jsonDocument.RootElement.GetProperty("status").GetInt32());
        Assert.Equal("Identity error", jsonDocument.RootElement.GetProperty("title").GetString());
        Assert.Equal(exception.StackTrace, jsonDocument.RootElement.GetProperty("detail").GetString());
        Assert.Equal("test-trace-id", jsonDocument.RootElement.GetProperty("traceId").GetString());

        // Verify errors array structure
        Assert.True(jsonDocument.RootElement.TryGetProperty("errors", out var errors));
        Assert.Equal(JsonValueKind.Array, errors.ValueKind);
        Assert.Equal(2, errors.GetArrayLength());

        // Check first error
        var firstError = errors[0];
        Assert.Equal("DuplicateEmail", firstError.GetProperty("code").GetString());
        Assert.Equal("Email is already taken", firstError.GetProperty("description").GetString());
    }

    [Fact]
    public async Task WriteResponseJson_UnhandledException_ReturnsExpectedJson()
    {
        // Arrange
        var exception = new InvalidOperationException("Something went wrong");

        SetMockStackTrace(exception);

        var context = CreateHttpContext(exception);

        // Act
        await InvokeMiddlewareDelegate(context);

        // Assert
        var responseJson = await ReadResponseJson(context);
        var jsonDocument = JsonDocument.Parse(responseJson);

        Assert.Equal(500, jsonDocument.RootElement.GetProperty("status").GetInt32());
        Assert.Equal("An error occurred on the server.", jsonDocument.RootElement.GetProperty("title").GetString());
        Assert.Equal(exception.StackTrace, jsonDocument.RootElement.GetProperty("detail").GetString());
        Assert.Equal("test-trace-id", jsonDocument.RootElement.GetProperty("traceId").GetString());
        Assert.Equal("Something went wrong", jsonDocument.RootElement.GetProperty("errors").GetString());
    }

    private static HttpContext CreateHttpContext(Exception? exception)
    {
        var context = new DefaultHttpContext();
        context.TraceIdentifier = "test-trace-id";
        context.Response.Body = new MemoryStream();

        var exceptionHandlerFeature = new Mock<IExceptionHandlerPathFeature>();
        exceptionHandlerFeature.Setup(f => f.Error).Returns(exception!);
        context.Features.Set(exceptionHandlerFeature.Object);

        return context;
    }

    private static async Task InvokeMiddlewareDelegate(HttpContext context)
    {
        // Mock the RequestDelegate that would be used inside UseExceptionHandler
        RequestDelegate handler = ctx =>
        {
            var exception = ctx.Features.Get<IExceptionHandlerPathFeature>()?.Error;
            ctx.Response.ContentType = "application/json";
            return ctx.Response.WriteAsync(JsonSerializer.Serialize(
                CustomExceptionHandlerMiddleware.GetResponseBody(exception, ctx)));
        };

        await handler(context);
    }

    private static async Task<string> ReadResponseJson(HttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }

    private static void SetMockStackTrace(Exception exception)
    {
        const string mockStackTrace =
            "   at TestNamespace.TestClass.TestMethod() in TestFile.cs:line 42\n   at Another.Method() in AnotherFile.cs:line 123";

        typeof(Exception)
            .GetField("_stackTraceString", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(exception, mockStackTrace);
    }
}