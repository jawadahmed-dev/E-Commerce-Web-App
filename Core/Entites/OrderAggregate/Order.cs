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

		public Order(string buyerEmail, ShipToAddress shipToAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderedItems, decimal subTotal)
		{
			BuyerEmail = buyerEmail;
			ShipToAddress = shipToAddress;
			DeliveryMethod = deliveryMethod;
			OrderedItems = orderedItems;
			this.SubTotal = subTotal;
		}

		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
		public ShipToAddress ShipToAddress { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
		public IReadOnlyList<OrderItem> OrderedItems { get; set; }
		public decimal SubTotal { get; set; }
		public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
		public string PaymentId { get; set; }
		public decimal GetTotal() 
		{
			return SubTotal + DeliveryMethod.Price;
		}


	}
}
