using Api.Exceptions;
using Api.Models;
using Core.Entites;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
	public class BasketController : BaseApiController
	{
		public IBasketRepository _basketRepository { get; set; }

		public BasketController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}

		[HttpGet]
		public async Task<ActionResult<Response<CustomerBasket>>> GetBasketAsync(string id)
		{
			var result = await _basketRepository.GetBasketAsync(id);

			if (result == null) throw new NotFoundException($"Coudn't Found Basket with Id : {id}");
			
			return Response<CustomerBasket>.Success(200, result);
		}

		/// <summary>
		/// Use this endpoint for both updation and addition of basket. 
		/// </summary>
		/// <returns>Returns the updated or newly added basket.</returns>
		[HttpPost]
		public async Task<ActionResult<Response<CustomerBasket>>> UpdateBasketAsync(CustomerBasket customerBasket)
		{
			var result = await _basketRepository.UpdateBasketAsync(customerBasket);

			if (result == null) throw new BadRequestException("Something went wrong!");

			return Response<CustomerBasket>.Success(200, result);
		}

		/// <summary>
		///  Deletes the basket from the redis database.
		/// </summary>
		/// <param name="id">It requires basket id.</param>
		/// <returns>It returns a bool for success/failure response.</returns>
		[HttpDelete]
		public async Task<ActionResult<Response<Boolean>>> DeleteBasketAsync(string id)
		{
			var result = await _basketRepository.DeleteBasketAsync(id);

			if (!result) throw new BadRequestException("Something went wrong!");

			return Response<Boolean>.Success(200, result);
		}
	}
}
