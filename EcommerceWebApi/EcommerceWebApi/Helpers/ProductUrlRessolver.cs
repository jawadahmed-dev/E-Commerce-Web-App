using AutoMapper;
using Core.Entites;
using EcommerceWebApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Helpers
{
	public class ProductUrlRessolver : IValueResolver<Product, ProductResponseModel, string>
	{
		private readonly IConfiguration _configuration;

		public ProductUrlRessolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(Product source, ProductResponseModel destination, string destMember, ResolutionContext context)
		{
			return _configuration["ApiUrl"] + source.PictureUrl;
		}
	}
}
