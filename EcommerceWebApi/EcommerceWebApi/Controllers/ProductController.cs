using Core.Interfaces.Repositories;
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
	public class ProductController : ControllerBase
	{
		private readonly DatabaseContext _databaseContext;
		private readonly IProductRepository _productRepository;

		public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductByIdAsync(int id) 
		{
			return Ok(await _productRepository.GetProductByIdAsync(id));
		}

		[HttpGet]
		public async Task<IActionResult> GetProductsAsync()
		{
			return Ok(await _productRepository.GetProductsAsync());
		}
	}
}
