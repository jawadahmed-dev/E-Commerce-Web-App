using Core.Interfaces.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class ResponseCacheService : IResponseCacheService
	{
		private readonly IDatabase _database;

		public ResponseCacheService(IConnectionMultiplexer redis)
		{
			_database = redis.GetDatabase();
		}

		public async Task CacheReponseAsync(string cacheKey, object response, TimeSpan timeSpan)
		{
			if (response == null) return;
			
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

			var jsonResponse = JsonSerializer.Serialize(response, options);

			await _database.StringSetAsync(cacheKey, jsonResponse, timeSpan);

		}

		public async Task<string> GetCacheReponseAsync(string cacheKey)
		{
			var cachedResponse = await _database.StringGetAsync(cacheKey);
			
			if(cachedResponse.IsNullOrEmpty) return null;

			return cachedResponse;
		}
	}
}
