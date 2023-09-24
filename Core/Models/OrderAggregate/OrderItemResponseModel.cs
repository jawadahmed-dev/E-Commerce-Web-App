using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.OrderAggregate
{
	public class OrderItemResponseModel
	{
		public OrderItemResponseModel(ProductItemOrderedResponseModel productItemOrdered, int quantity, int price, int id)
		{
			this.productItemOrdered = productItemOrdered;
			Quantity = quantity;
			Price = price;
			Id = id;
		}

		public int Id { get; set; }
		public ProductItemOrderedResponseModel productItemOrdered { get; set; }
		public int Quantity { get; set; }
		public int Price { get; set; }
	}
}
