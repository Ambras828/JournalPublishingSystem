using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Core.DTOs;
using Infrastructure.Exceptions;
using Infrastructure.CustomeExceptions;


namespace Infrastructure.ExceptionHandling
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context,Exception ex)
        {
            var (statusCode, message, logLevel)= ex switch
            {
                ValidationException => (HttpStatusCode.BadRequest, "Validation failed.",LogLevel.Warning),
                ArgumentNullException => (HttpStatusCode.BadRequest, "A required argument was null.", LogLevel.Warning),
                ArgumentException => (HttpStatusCode.BadRequest, "Invalid argument provided.", LogLevel.Warning),
                KeyNotFoundException => (HttpStatusCode.NotFound, "The requested resource was not found.", LogLevel.Warning),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized access.", LogLevel.Warning),
                AuthencationException=>(HttpStatusCode.Unauthorized,"invalid Credentials",LogLevel.Warning),
                CountryAlreadyExistsException=>(HttpStatusCode.BadRequest,"Data Already Exists",LogLevel.Warning),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.", LogLevel.Error)
                
            };

            _logger.Log(logLevel, ex, "An error occurred: {Message}", ex.Message);
            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = message,
                TraceId = context.TraceIdentifier,
                Details = ex.Message // Can be hidden in production
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));

        }
    }
}
