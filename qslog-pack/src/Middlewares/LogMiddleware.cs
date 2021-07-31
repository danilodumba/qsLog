using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using qsLogPack.Exceptions;
using qsLogPack.Services.Interfaces;

namespace qsLogPack.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogService logService)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await SetError(httpContext, ex, logService);
            }
        }

        private async Task SetError(HttpContext context, Exception ex, ILogService logService)
        {
            string error;
            try
            {
                var id = await logService.Error(ex);
                error = $"Ocorreu um erro inesperado no sistema. Veja o log {id}.";
            }
            catch (LogException lx)
            {
                error = $"Nao foi possivel logar o erro no sistema de log. Veja o arquivo local de log para mais detalhes. Error: {ex.Message}, erro Log: {lx.Message}";
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(error);
        }
    }

    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseQsLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}