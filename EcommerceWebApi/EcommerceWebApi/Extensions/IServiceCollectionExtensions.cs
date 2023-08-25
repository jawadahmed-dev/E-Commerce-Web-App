using EcommerceWebApi.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EcommerceWebApi.Extensions
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddApiServices(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddControllers();
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = action =>
				{
					var errors = action.ModelState
					.Where(x => x.Value.Errors.Count > 0)
					.SelectMany(x => x.Value.Errors)
					.Select(x => x.ErrorMessage).ToList();

					var response = new ApiValidationException(errors);
					return new BadRequestObjectResult(response);
				};
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcommerceWebApi", Version = "v1" });
			});

			return services;
		}
	}
}
