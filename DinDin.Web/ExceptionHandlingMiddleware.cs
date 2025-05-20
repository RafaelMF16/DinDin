using System.Net;
using System.Text.Json;
using DinDin.Domain.Constantes;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DinDin.Web
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path,
                Status = (int)HttpStatusCode.InternalServerError,
                Title = ApplicationConstants.INTERNAL_SERVER_ERROR_TITLE,
                Detail = exception.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            switch (exception)
            {
                case ValidationException validationException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = ApplicationConstants.VALIDATION_EXCEPTION_TITLE;
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    problemDetails.Extensions[ApplicationConstants.VALIDATION_EXTENSIONS_NAME] = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());
                    break;

                case BadHttpRequestException badRequestEx:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = "Requisição malformada.";
                    problemDetails.Detail = badRequestEx.Message;
                    break;

                default:
                    logger.LogError(exception, "Erro inesperado");
                    break;
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status ?? 500;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(problemDetails, options);

            await context.Response.WriteAsync(json);
        }
    }
}