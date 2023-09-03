using AutoMapper;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceWebApi.Helpers;
using EcommerceWebApi.Models;

namespace EcommerceWebApi.MappingProfiles
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductResponseModel>()
				.ForMember(x => x.ProductBrandName, o => o.MapFrom(x => x.ProductBrand.Name))
				.ForMember(x => x.ProductTypeName, o => o.MapFrom(x => x.ProductType.Name))
				.ForMember(x => x.PictureUrl, o => o.MapFrom<ProductUrlRessolver>());

			CreateMap<ProductBrand, ProductBrandResponseModel>().ReverseMap();
			CreateMap<ProductType, ProductTypeResponseModel>().ReverseMap();
		}
	}
}
