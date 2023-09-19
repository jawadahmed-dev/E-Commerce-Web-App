using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IResponseCacheService
	{
		Task CacheReponseAsync(string cacheKey, object response, TimeSpan timeSpan);
		Task<string> GetCacheReponseAsync(string cacheKey);
	}
}
