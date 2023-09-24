using AutoMapper;
using Core.Entites.OrderAggregate;
using Core.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, OrderResponseModel>()
			.ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal))
			.ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
			.ForMember(dest => dest.ShipToAddress, opt => opt.MapFrom(src => src.ShipToAddress))
			.ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod))
			.ForMember(dest => dest.OrderedItems, opt => opt.MapFrom(src => src.OrderedItems));

			CreateMap<CreateShipToAddressModel, ShipToAddress>();
			CreateMap<OrderItem, OrderItemResponseModel>();
			CreateMap<ProductItemOrdered, ProductItemOrderedResponseModel>();
			CreateMap<DeliveryMethod, DeliveryMethodResponseModel>();
			CreateMap<OrderStatus, OrderStatusResponseModel>();
		}
		
	}
}
