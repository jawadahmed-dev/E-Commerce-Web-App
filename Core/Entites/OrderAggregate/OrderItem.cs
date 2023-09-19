using Core.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites.OrderAggregate
{
	public class OrderItem : BaseEntity
	{
		public OrderItem()
		{
		}

		public OrderItem(ProductItemOrdered productItemOrdered, int quantity, int price)
		{
			this.productItemOrdered = productItemOrdered;
			Quantity = quantity;
			Price = price;
		}

		public ProductItemOrdered productItemOrdered { get; set; }
		public int Quantity { get; set; }
		public int Price { get; set; }
	}
}
