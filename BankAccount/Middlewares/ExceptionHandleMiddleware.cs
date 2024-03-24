using System.Net;
using BankAccount.Exceptions;
using Newtonsoft.Json;

namespace BankAccount.Middlewares;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IWebHostEnvironment environment)
    {
        httpContext.Request.EnableBuffering();

        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync<T>(
        HttpContext httpContext,
        T ex) where T : Exception
    {;
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected

        ErrorInfo result = null;
        if (ex is BusinessException businessException)
        {
            result = businessException.ErrorInfo;
        }
        else
            result = new ErrorInfo
            {
                Code = (int)code,
                UserMessage = ex.Message,
                TechnicalMessage = ex.StackTrace ?? string.Empty
            };

        var jsonResult = JsonConvert.SerializeObject(result);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = result.Code;

        return httpContext.Response.WriteAsync(jsonResult);
    }
}