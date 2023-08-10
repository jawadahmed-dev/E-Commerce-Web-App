using AutoMapper;
using Core.Entites;
using Core.Interfaces.Repositories;
using Core.Specifications;
using EcommerceWebApi.DTOs;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
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
			return Ok(_mapper.Map<List<ProductDTO>>(await _productRepository.GetByIdAsync(id)));
		}

		[HttpGet]
		public async Task<IActionResult> GetProductsAsync()
		{
			var spec = new GetProductsWithTypeAndBrandSpecification();

			return Ok(_mapper.Map<List<ProductDTO>>(await _productRepository.ListAsync(spec)));
		}

		[HttpGet("Types")]
		public async Task<IActionResult> GetProductTypesAsync()
		{
			return Ok(await _productRepository.ListAllAsync());
		}

		[HttpGet("Brands")]
		public async Task<IActionResult> GetProductBrandsAsync()
		{
			return Ok(await _productRepository.ListAllAsync());
		}
	}
}
