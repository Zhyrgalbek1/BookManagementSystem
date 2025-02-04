﻿using System.Net;
using WebApi.Dtos;

namespace WebApi.Middlewere
{
    public class ExceptionHandlingMiddlwere
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddlwere> _logger;

        public ExceptionHandlingMiddlwere(RequestDelegate next,
            ILogger<ExceptionHandlingMiddlwere> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExeptionAsync(httpContext, ex.Message,
                      HttpStatusCode.InternalServerError, "MiddlewereErrorHandler");
                throw;
            }
        }

        private async Task HandleExeptionAsync(HttpContext context,
            string exMassage, HttpStatusCode httpStatusCode, string massage)
        {
            _logger.LogError(exMassage);

            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            ErrorDto errorDto = new()
            {
                Message = massage,
                StatusCode = (int)httpStatusCode,
            };

            await response.WriteAsJsonAsync(errorDto);
        }
    }
}
