using Core.Entites;
using Core.Models.Product;
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

		public GetProductsWithTypeAndBrandSpecification(GetProductsModel getProductsModel) 
			: base (x => 
				(string.IsNullOrEmpty(getProductsModel.Search) || x.Name.Contains(getProductsModel.Search)) && 
				(!getProductsModel.BrandId.HasValue || x.ProductBrandId == getProductsModel.BrandId) && 
				(!getProductsModel.TypeId.HasValue || x.ProductTypeId == getProductsModel.TypeId)
			)
		{
			AddInclude(x => x.ProductBrand);
			AddInclude(x => x.ProductType);
			ApplyPaging(getProductsModel.PageSize, (getProductsModel.PageIndex - 1) * getProductsModel.PageSize);

			if (!string.IsNullOrEmpty(getProductsModel.Sort))
			{
				switch (getProductsModel.Sort)
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
