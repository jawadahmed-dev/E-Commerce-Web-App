using Core.Entites;
using Core.Entites.OrderAggregate;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class OrderService : IOrderService
	{
		public IUnitOfWork _unitOfWork { get; set; }
		public IBasketRepository _basketRepository { get; set; }

		public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShipToAddress shipToAddress)
		{
			var basket = await _basketRepository.GetBasketAsync(basketId);

			var items = new List<OrderItem>();

			foreach (var basketItem in basket.BasketItems)
			{
				var product = await _unitOfWork.Repository<Product>().GetByIdAsync(basketItem.Id);
				var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
				var orderItem = new OrderItem(productItemOrdered, basketItem.Quantity, (int)product.Price);
				items.Add(orderItem);
			}

			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

			var orderSubTotal = items.Sum(i => i.Quantity * i.Price);

			var order = new Order(buyerEmail, shipToAddress, deliveryMethod, items, orderSubTotal);

			_unitOfWork.Repository<Order>().Add(order);

			var result = await _unitOfWork.CompleteAsync();

			if (result < 0) return null;

			await _basketRepository.DeleteBasketAsync(basketId);

			return order;
		}

		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
		{
			return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
		}

		public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
		{
			var spec = new OrdersWithItemAndOrderingSpecification(buyerEmail);

			return await _unitOfWork.Repository<Order>().ListAsync(spec);
		}

		public async Task<Order> GetOrdersByIdAsync(int orderId, string buyerEmail)
		{
			var spec = new OrdersWithItemAndOrderingSpecification(orderId, buyerEmail);

			return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
		}
	}
}
