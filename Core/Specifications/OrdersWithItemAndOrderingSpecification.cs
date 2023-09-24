using Core.Entites.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class OrdersWithItemAndOrderingSpecification : BaseSpecification<Order>
	{
		public OrdersWithItemAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
		{
			AddInclude(o => o.OrderedItems);
			AddInclude(o => o.DeliveryMethod);
			AddOrderByDesc(o => o.OrderDate);
		}

		public OrdersWithItemAndOrderingSpecification(int id, string email) : base(o => o.BuyerEmail == email && o.Id == id)
		{
			AddInclude(o => o.OrderedItems);
			AddInclude(o => o.DeliveryMethod);
			AddOrderByDesc(o => o.OrderDate);
		}
	}
}
