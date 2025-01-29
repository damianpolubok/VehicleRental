using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace VehicleRental.API.Validation
{
    public class ProblemDetailsExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ProblemDetailsExceptionHandler> _logger;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ProblemDetailsExceptionHandler(
            ILogger<ProblemDetailsExceptionHandler> logger,
            ProblemDetailsFactory problemDetailsFactory)
        {
            _logger = logger;
            _problemDetailsFactory = problemDetailsFactory;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception occurred.");

            var statusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ValidationException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var problemDetails = _problemDetailsFactory.CreateProblemDetails(
                context,
                statusCode,
                title: exception.GetType().Name,
                detail: exception.Message,
                instance: context.Request.GetDisplayUrl());

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            var responseJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(responseJson, cancellationToken);

            return true;
        }
    }
}