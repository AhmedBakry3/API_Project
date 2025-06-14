using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    internal class CacheAttribute(int DurationInSec = 300) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Create Cache Key
            string CacheKey = CreateCacheKey(context.HttpContext.Request);

            //Search For Value With Cache Key
            ICacheService CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheValue = await CacheService.GetAsync(CacheKey);

            //Check If Value Is Not Null
            if(CacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            //Check if Value Is Null
            //Invoke Next
            var ExecutedContext =  await next.Invoke();

            //Set Value With Cache Key
            if(ExecutedContext.Result is OkObjectResult result)
            {
                await CacheService.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(DurationInSec));
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path + '?');

            foreach(var Item in request.Query.OrderBy(Q => Q.Key))
            {
                key.Append($"{Item.Key}={Item.Value}&");
            }

            return key.ToString();
        }
    }
}
