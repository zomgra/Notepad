using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Notepad.Domain.Exceptions;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using FluentValidation;

namespace Notepad.API.Middleware
{
    public class ApplicationExceprionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ApplicationExceprionHandlerMiddleware(RequestDelegate next) =>
        _next = next;

        public async Task Invoke(HttpContext httpContext,
            ILogger<ApplicationExceprionHandlerMiddleware> logger,
            ProblemDetailsFactory problemDetailsFactory)
        {
            try
            {
                logger.LogError("Error handling started for request in path {RequestPath}", httpContext.Request.Path.Value);
                await _next.Invoke(httpContext);
            }
            catch (Exception exception)
            {
                logger.LogError(
                exception,
                    "Error has happened with {RequestPath}, the message is {ErrorMessage}",
                    httpContext.Request.Path.Value, exception.Message);

                ProblemDetails problemDetails;
                switch (exception)
                {
                    case UnauthorizedAccessException unauthorizedAccessException:
                        problemDetails = problemDetailsFactory.CreateFrom(httpContext, unauthorizedAccessException);
                        break;
                    case ValidationException validationException:
                        problemDetails = problemDetailsFactory.CreateFrom(httpContext, validationException);
                        logger.LogInformation(validationException, "Somebody sent invalid request, oops");
                        break;
                    case NotepadNotFoundException notepadNotFoundException:
                        problemDetails = problemDetailsFactory.CreateFrom(httpContext, notepadNotFoundException);
                        logger.LogError(notepadNotFoundException, "Domain exception occured");
                        break;
                    default:
                        problemDetails = problemDetailsFactory.CreateProblemDetails(
                            httpContext, StatusCodes.Status500InternalServerError, "Unhandled error! Please contact us.");
                        logger.LogError(exception, "Unhandled exception occured");
                        break;
                }

                httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, problemDetails.GetType());
            }
        }
    }
}
