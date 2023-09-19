using Core.Entites;
using Core.Entites.OrderAggregate;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class OrderService : IOrderService
	{
		public IGenericRepository<Order> _orderRepository { get; set; }
		public IGenericRepository<DeliveryMethod> _deliveryMethodRepository { get; set; }
		public IGenericRepository<Product> _productRepository { get; set; }
		public IBasketRepository _basketRepository { get; set; }

		public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<Product> productRepository, IBasketRepository basketRepository, IGenericRepository<DeliveryMethod> deliveryMethodRepository)
		{
			_orderRepository = orderRepository;
			_productRepository = productRepository;
			_basketRepository = basketRepository;
			_deliveryMethodRepository = deliveryMethodRepository;
		}

		public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shipToAddress)
		{
			var basket = await _basketRepository.GetBasketAsync(basketId);

			var items = new List<OrderItem>();
			foreach (var basketItem in basket.BasketItems)
			{
				var product = await _productRepository.GetByIdAsync(basketItem.Id);
				var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
				var orderItem = new OrderItem(productItemOrdered, basketItem.Quantity, (int)product.Price);
				items.Add(orderItem);
			}

			var deliveryMethod = await _deliveryMethodRepository.GetByIdAsync(deliveryMethodId);

			var orderSubTotal = items.Sum(i => i.Quantity * i.Price);

			var order = new Order(buyerEmail, shipToAddress, deliveryMethod, items, orderSubTotal);


			return order;
		}

		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
		{
			throw new NotImplementedException();
		}

		public Task<Order> GetOrdersByIdAsync(int orderId, string buyerEamil)
		{
			throw new NotImplementedException();
		}
	}
}
