using SimpleBanking.Domain.Features.Balances.Exceptions;

namespace SimpleBanking.API.Middlewares.ErrorMiddlewares;

/// <summary>
/// Represents a handled response
/// </summary>
public record HandledResponse
{
    /// <summary>
    /// Represents a status code
    /// </summary>
    public required int StatusCode { get; set; }

    /// <summary>
    /// The response content. This field must be serialized
    /// </summary>
    public required object Content { get; set; }
}

/// <summary>
/// Generic Error
/// </summary>
public class BaseError
{
    /// <summary>
    /// Represents a status code
    /// </summary>
    public string Kind { get; set; } = string.Empty;

    /// <summary>
    /// The message describing the error
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// More details about the error
    /// </summary>
    public string Details { get; set; } = string.Empty;
}

/// <summary>
/// Handle a generic error
/// </summary>
public class GenericHandleExceptionMiddleware(ILogger<HandleExceptionMiddleware> logger, RequestDelegate next)
{
    /// <summary>
    /// Invoke the Handler
    /// </summary>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            logger.LogError(ex.StackTrace);

            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Something went wrong"
            });
        }
    }
}

/// <summary>
/// Apply a Generic Error handler. Handle `Exception` using a generic message.
/// </summary>
public class HandleExceptionMiddleware(ILogger<HandleExceptionMiddleware> logger, RequestDelegate next)
{
    /// <summary>
    /// Invoke the Handler
    /// </summary>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            logger.LogError(ex.StackTrace);

            var response = ex.HandleGenericException();


            if (response.StatusCode == 500)
            {
                throw;
            }

            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsJsonAsync(response.Content);
        }
    }
}

public static class HandleExceptionMiddlewareExtensions
{
    public static IApplicationBuilder AddErrorHandlers(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsProduction())
        {
            app.UseMiddleware<GenericHandleExceptionMiddleware>();
        }

        app.UseMiddleware<HandleExceptionMiddleware>();

        return app;

    }

    public static HandledResponse HandleGenericException(this Exception ex)
      => ex switch
      {
          TransferException e => e.HandleTransferError(),

          _ => new()
          {
              Content = "Something went wrong on our side",
              StatusCode = 500
          }

      };
}
