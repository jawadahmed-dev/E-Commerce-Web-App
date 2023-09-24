using Api.Exceptions;
using Api.Models;
using AutoMapper;
using Core.Entites.OrderAggregate;
using Core.Extensions;
using Core.Interfaces.Services;
using Core.Models.OrderAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
	public class OrdersController : BaseApiController
	{
		public IOrderService _orderService { get; set; }
		public IMapper _mapper { get; set; }

		public OrdersController(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<ActionResult<Response<OrderResponseModel>>> CreateOrderAsync(CreateOrderModel createOrderModel)
		{
			var buyerEmail = User.GetEmailFromPrincipal();

			if (buyerEmail == null) throw new BadRequestException("User doesn't contain email claim.");

			var shipToAddress = _mapper.Map<ShipToAddress>(createOrderModel.createShipToAddressModel);

			var order = await _orderService.CreateOrderAsync(buyerEmail, createOrderModel.DeliveryMethodId, createOrderModel.BasketId, shipToAddress);

			var orderResponse = _mapper.Map<OrderResponseModel>(order);

			return Ok(Response<OrderResponseModel>.Success(200, orderResponse));

		}

		[HttpGet]
		public async Task<ActionResult<Response<OrderResponseModel>>> GetOrdersForUser()
		{
			var buyerEmail = User.GetEmailFromPrincipal();

			var order = await _orderService.GetOrderForUserAsync(buyerEmail);

			var orderResponse = _mapper.Map<OrderResponseModel>(order);

			return Ok(Response<OrderResponseModel>.Success(200, orderResponse));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Response<OrderResponseModel>>> GetOrdersByIdForUser(int id)
		{
			var buyerEmail = User.GetEmailFromPrincipal();

			var order = await _orderService.GetOrdersByIdAsync(id, buyerEmail);

			var orderResponse = _mapper.Map<OrderResponseModel>(order);

			return Ok(Response<OrderResponseModel>.Success(200, orderResponse));
		}

		[HttpGet("delivery-methods")]
		public async Task<ActionResult<Response<IReadOnlyList<DeliveryMethodResponseModel>>>> GetDeliveryMethods()
		{

			var deliveryMethods = await _orderService.GetDeliveryMethodAsync();

			var deliveryMethodsResponse = _mapper.Map<IReadOnlyList<DeliveryMethodResponseModel>>(deliveryMethods);

			return Ok(Response<IReadOnlyList<DeliveryMethodResponseModel>>.Success(200, deliveryMethodsResponse));
		}
	}
}