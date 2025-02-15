using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinDin.Web
{
    public static class ProblemDetailsExtensions
    {
        public static void UseProblemDetailsExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        var problemDetails = new ProblemDetails
                        {
                            Instance = context.Request.HttpContext.Request.Path
                        };

                        if (exception is ValidationException validationException)
                        {
                            problemDetails.Title = "Validation Error";
                            problemDetails.Status = StatusCodes.Status400BadRequest;
                            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                            problemDetails.Detail = validationException.StackTrace;
                            problemDetails.Extensions["ValidationError"] = validationException.Errors
                            .GroupBy(nomePropriedade => nomePropriedade.PropertyName, mensagemErro => mensagemErro.ErrorMessage)
                            .ToDictionary(x => x.Key, x => x.ToList());
                        }
                        else if (exception is BadHttpRequestException badHttpRequestException)
                        {
                            problemDetails.Title = "The request is invalid";
                            problemDetails.Status = StatusCodes.Status400BadRequest;
                            problemDetails.Detail = badHttpRequestException.Message;
                        }
                        else
                        {
                            var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                            logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");
                            problemDetails.Title = exception.Message;
                            problemDetails.Status = StatusCodes.Status500InternalServerError;
                            problemDetails.Detail = exception.Demystify().ToString();
                        }
                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.ContentType = "application/problem+json";
                        var json = JsonConvert.SerializeObject(problemDetails);
                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }
    }
}