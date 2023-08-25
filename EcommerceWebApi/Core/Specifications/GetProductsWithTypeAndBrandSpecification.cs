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

		public GetProductsWithTypeAndBrandSpecification(ProductSpecParams productParams) 
			: base (x => 
				(!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
				(!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
			)
		{
			AddInclude(x => x.ProductBrand);
			AddInclude(x => x.ProductType);
			ApplyPaging(productParams.PageSize, (productParams.PageIndex - 1) * productParams.PageSize);

			if (!string.IsNullOrEmpty(productParams.Sort))
			{
				switch (productParams.Sort)
				{
					case "priceAsc":
						AddOrderBy(x => x.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(x => x.Price);
						break;
					default:
						AddOrderBy(x => x.Name);
						break;
				}
			}
		}

		public GetProductsWithTypeAndBrandSpecification(int id) : base(x => x.Id == id)
		{
			AddInclude(x => x.ProductBrand);
			AddInclude(x => x.ProductType);
		}
	}
}
