using Core.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites.OrderAggregate
{
	public class Order : BaseEntity
	{
		public Order()
		{
		}

		public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderedItems, decimal subTotal)
		{
			BuyerEmail = buyerEmail;
			ShipToAddress = shipToAddress;
			DeliveryMethod = deliveryMethod;
			OrderedItems = orderedItems;
			this.subTotal = subTotal;
		}

		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
		public Address ShipToAddress { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
		public IReadOnlyList<OrderItem> OrderedItems { get; set; }
		public decimal subTotal { get; set; }
		public OrderStatus orderStatus { get; set; } = OrderStatus.Pending;
		public string PaymentId { get; set; }
		public decimal GetTotal() 
		{
			return subTotal + DeliveryMethod.Price;
		}


	}
}
