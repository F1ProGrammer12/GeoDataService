using GeoDataService.Logic.Exceptions;
using System.Net;

namespace GeoDataService.Host.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private const string ErrorResponseContentType = "text/plain";

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (ValidationRequestException vrex)
        {
            context.Response.ContentType = ErrorResponseContentType;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(vrex.Message);
        }
        catch (NotFoundException nfex)
        {
            context.Response.ContentType = ErrorResponseContentType;
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync(nfex.Message);
        }
        catch
        {
            context.Response.ContentType = ErrorResponseContentType;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("InternalError");
        }
    }
}