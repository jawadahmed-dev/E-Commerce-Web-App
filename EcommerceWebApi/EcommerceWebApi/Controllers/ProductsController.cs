using AutoMapper;
using Core.Entites;
using Core.Interfaces.Repositories;
using Core.Specifications;
using EcommerceWebApi.DTOs;
using EcommerceWebApi.Response;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly DatabaseContext _databaseContext;
		private readonly IGenericRepository<Product> _productRepository;
		private readonly IGenericRepository<ProductType> _productTypeRepository;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<ProductBrand> _productBrandRepository;

		public ProductsController(IMapper mapper, IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository, IGenericRepository<Product> productRepository)
		{
			_mapper = mapper;
			_productBrandRepository = productBrandRepository;
			_productTypeRepository = productTypeRepository;
			_productRepository = productRepository;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductByIdAsync(int id) 
		{
			var spec = new GetProductsWithTypeAndBrandSpecification(id);

			return Ok(_mapper.Map<ProductDTO>(await _productRepository.GetEntityWithSpec(spec)));
		}

		[HttpGet]
		public async Task<IActionResult> GetProductsAsync([FromQuery] ProductSpecParams productParams)
		{
			var spec = new GetProductsWithTypeAndBrandSpecification(productParams);

			var products = _mapper.Map<List<ProductDTO>>(await _productRepository.ListAsync(spec));

			var countSpec = new GetFilteredProductsCountSpecification(productParams);

			var count = await _productRepository.CountAsync(countSpec);

			return Ok(new Pagination<ProductDTO>(productParams.PageIndex, productParams.PageSize, count, products));
		}

		[HttpGet("Types")]
		public async Task<IActionResult> GetProductTypesAsync()
		{
			return Ok(_mapper.Map<IEnumerable<ProductTypeDTO>>(await _productTypeRepository.ListAllAsync()));
		}

		[HttpGet("Brands")]
		public async Task<IActionResult> GetProductBrandsAsync()
		{
			return Ok(_mapper.Map<IEnumerable<ProductBrandDTO>>(await _productBrandRepository.ListAllAsync()));
		}
	}
}
