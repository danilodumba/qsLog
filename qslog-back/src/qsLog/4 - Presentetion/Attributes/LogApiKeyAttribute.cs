using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using qsLog.Domains.Projects.Repository;

namespace qsLog.Presentetion.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Method)]
    public class LogApiKeyAttribute : AllowAnonymousAttribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {   
            if (!context.HttpContext.Request.Query.TryGetValue("api-key", out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey não encontrada"
                };
                return;
            }

            var projectRepository = context.HttpContext.RequestServices.GetService(typeof(IProjectRepository)) as IProjectRepository;
            var canEnter = false;
            if (Guid.TryParse(extractedApiKey, out var apiKey))
            {
                canEnter = projectRepository.ApiKeyExists(apiKey);
            }

            if (!canEnter)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Acesso não autorizado"
                };
                return;
            }

            await next();
        }
    }
}