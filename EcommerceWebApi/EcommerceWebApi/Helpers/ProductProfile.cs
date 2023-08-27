using AutoMapper;
using EcommerceWebApi.DTOs;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceWebApi.Helpers;

namespace EcommerceWebApi.MappingProfiles
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductDTO>()
				.ForMember(x => x.ProductBrandName, o => o.MapFrom(x => x.ProductBrand.Name))
				.ForMember(x => x.ProductTypeName, o => o.MapFrom(x => x.ProductType.Name))
				.ForMember(x => x.PictureUrl, o => o.MapFrom<ProductUrlRessolver>());

			CreateMap<ProductBrand, ProductBrandDTO>().ReverseMap();
			CreateMap<ProductType, ProductTypeDTO>().ReverseMap();
		}
	}
}
