using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface IBasketRepository
	{
		Task<CustomerBasket> GetBasketAsync(string customerBasketId);
		Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
		Task<bool> DeleteBasketAsync(string customerBasketId);
	}
}
