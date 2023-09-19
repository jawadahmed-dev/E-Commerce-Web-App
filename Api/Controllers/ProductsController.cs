using AutoMapper;
using Core.Entites;
using Core.Interfaces.Repositories;
using Core.Specifications;
using Api.Exceptions;
using Api.Models;
using Api.Response;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Product;
using Api.Filters;

namespace Api.Controllers
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

		/// <summary>Get individual product by its id.</summary>

		[HttpGet("{id}")]
		public async Task<ActionResult<Response<ProductResponseModel>>> GetProductByIdAsync(int id) 
		{
			var spec = new GetProductsWithTypeAndBrandSpecification(id);
			var data = await _productRepository.GetEntityWithSpec(spec);

			if (data == null) throw new NotFoundException();

			return Ok(Response<ProductResponseModel>.Success(200, _mapper.Map<ProductResponseModel>(data)));
		}

		/// <summary>Get list of products in paginated form.</summary>

		[Cached(500)]
		[HttpGet]
		public async Task<ActionResult<Response<Pagination<ProductResponseModel>>>> GetProductsAsync([FromQuery] GetProductsModel getProductsModel)
		{
			var spec = new GetProductsWithTypeAndBrandSpecification(getProductsModel);

			var products = _mapper.Map<List<ProductResponseModel>>(await _productRepository.ListAsync(spec));

			var countSpec = new GetFilteredProductsCountSpecification(getProductsModel);

			var count = await _productRepository.CountAsync(countSpec);

			return Ok(Response<Pagination<ProductResponseModel>>.Success(200, new Pagination<ProductResponseModel>(getProductsModel.PageIndex, getProductsModel.PageSize, count, products)));
		}

		/// <summary>Get list of product types.</summary>

		[HttpGet("Types")]
		public async Task<ActionResult<Response<IEnumerable<ProductTypeResponseModel>>>> GetProductTypesAsync()
		{
			return Ok(Response<IEnumerable<ProductTypeResponseModel>>.Success(200, _mapper.Map<IEnumerable<ProductTypeResponseModel>>(await _productTypeRepository.ListAllAsync())));
		}

		/// <summary>Get list of product brands.</summary>

		[HttpGet("Brands")]
		public async Task<ActionResult<Response<IEnumerable<ProductBrandResponseModel>>>> GetProductBrandsAsync()
		{
			return Ok(Response<IEnumerable<ProductBrandResponseModel>>.Success(200, _mapper.Map<IEnumerable<ProductBrandResponseModel>>(await _productBrandRepository.ListAllAsync())));
		}
	}
}
