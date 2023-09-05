using Core.Entites;
using Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class GetFilteredProductsCountSpecification :  BaseSpecification<Product>
	{
		public GetFilteredProductsCountSpecification(GetProductsModel getProductsModel) 
			: base (x =>
			(string.IsNullOrEmpty(getProductsModel.Search) || x.Name.Contains(getProductsModel.Search)) &&
				(!getProductsModel.BrandId.HasValue || x.ProductBrandId == getProductsModel.BrandId ) &&
				(!getProductsModel.TypeId.HasValue || x.ProductTypeId == getProductsModel.TypeId )
			)
		{
			
		}
	}
}
