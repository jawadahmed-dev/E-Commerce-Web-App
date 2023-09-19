using Core.Entites.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IOrderService
	{
		Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId,
			Address shipToAddress);
		Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
		Task<Order> GetOrdersByIdAsync(int orderId, string buyerEamil);
		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
	}
}
