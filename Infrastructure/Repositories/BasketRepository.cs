using Core.Entites;
using Core.Interfaces.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		public IDatabase _redis { get; set; }

		public BasketRepository(IConnectionMultiplexer redis)
		{
			_redis = redis.GetDatabase();
		}

		public async Task<bool> DeleteBasketAsync(string customerBasketId)
		{
			return await _redis.KeyDeleteAsync(customerBasketId);
		}

		public async Task<CustomerBasket> GetBasketAsync(string customerBasketId)
		{
			var data = await _redis.StringGetAsync(customerBasketId);
			return string.IsNullOrEmpty(data) ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
		}

		public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
		{
			var isUpdated = await _redis.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize<CustomerBasket>(customerBasket), TimeSpan.FromDays(5));

			if (!isUpdated) return null;

			return await GetBasketAsync(customerBasket.Id);
		}
	}
}
