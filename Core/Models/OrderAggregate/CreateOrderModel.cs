using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.OrderAggregate
{
	public class CreateOrderModel
	{
		public string BasketId { get; set; }
		public int DeliveryMethodId { get; set; }
		public CreateShipToAddressModel createShipToAddressModel { get; set; }
	}
}
