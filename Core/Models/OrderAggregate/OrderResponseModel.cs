using Core.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.OrderAggregate
{
	public class OrderResponseModel
	{
		public OrderResponseModel()
		{
		}

		public OrderResponseModel(int id, string buyerEmail, DateTimeOffset orderDate, ShipToAddressResponseModel shipToAddress, DeliveryMethodResponseModel deliveryMethod, IReadOnlyList<OrderItemResponseModel> orderedItems, decimal subTotal, OrderStatusResponseModel orderStatus, string paymentId)
		{
			Id = id;
			BuyerEmail = buyerEmail;
			OrderDate = orderDate;
			ShipToAddress = shipToAddress;
			DeliveryMethod = deliveryMethod;
			OrderedItems = orderedItems;
			this.SubTotal = subTotal;
			this.OrderStatus = orderStatus;
			PaymentId = paymentId;
		}

		public int Id { get; set; }
		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
		public ShipToAddressResponseModel ShipToAddress { get; set; }
		public DeliveryMethodResponseModel DeliveryMethod { get; set; }
		public IReadOnlyList<OrderItemResponseModel> OrderedItems { get; set; }
		public decimal SubTotal { get; set; }
		public OrderStatusResponseModel OrderStatus { get; set; } = OrderStatusResponseModel.Pending;
		public string PaymentId { get; set; }
	}
}
