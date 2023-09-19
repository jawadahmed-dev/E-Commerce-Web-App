using Api.Models;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Filters
{
	public class CachedAttribute : Attribute, IAsyncActionFilter
	{
		public int TimeToLiveSeconds { get; set; }

		public CachedAttribute(int timeToLiveSeconds)
		{
			TimeToLiveSeconds = timeToLiveSeconds;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

			var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
			var cacheResponse = await cacheService.GetCacheReponseAsync(cacheKey);
			if (!string.IsNullOrEmpty(cacheResponse))
			{
				var contentResult = new ContentResult
				{
					Content = cacheResponse,
					ContentType = "application/json",
					StatusCode = 200
				};

				context.Result = contentResult;
				return;
			}
			else 
			{
				var executedContext = await next();

				if (executedContext.Result is OkObjectResult okObjectResult) 
				{
					await cacheService.CacheReponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(TimeToLiveSeconds));
				}
			}
		}

		private string GenerateCacheKeyFromRequest(HttpRequest request)
		{
			var keyBuilder = new StringBuilder();
			keyBuilder.Append($"{request.Path.Value}");

			foreach (var keyValuePair in request.Query.OrderBy(q => q.Key))
			{
				keyBuilder.Append($"|{keyValuePair.Key}-{keyValuePair.Value}");
			}

			return keyBuilder.ToString();
		}
	}
}
