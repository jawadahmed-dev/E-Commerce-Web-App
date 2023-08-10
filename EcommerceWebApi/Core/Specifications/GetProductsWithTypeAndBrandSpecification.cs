using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class GetProductsWithTypeAndBrandSpecification : BaseSpecification<Product>
	{
		public GetProductsWithTypeAndBrandSpecification()
		{
			AddInclude(x => x.ProductBrand);
			AddInclude(x => x.ProductType);
		}
	}
}
