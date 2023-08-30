using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.DTOs
{
	public class ProductDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public float Price { get; set; }
		public string PictureUrl { get; set; }
		public string ProductTypeName { get; set; }
		public string ProductBrandName { get; set; }
	}
}
