﻿using Core.Entites;
using Core.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entites
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public float Price { get; set; }
		public string PictureUrl { get; set; }
		public int ProductTypeId { get; set; }
		public ProductType ProductType { get; set; }
		public int ProductBrandId { get; set; }
		public ProductBrand ProductBrand { get; set; }
	}
}
