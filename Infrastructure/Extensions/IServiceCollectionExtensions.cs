using Core.Entites;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
		{

			services.AddSingleton<IConnectionMultiplexer>(o => 
			{
				var config = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"),true);
				return ConnectionMultiplexer.Connect(config);
			});
			services.AddDbContext<DatabaseContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("Default"));
			});
			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.User.RequireUniqueEmail = false;
			})
			.AddEntityFrameworkStores<DatabaseContext>();

			services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
			services.AddScoped<IBasketRepository,BasketRepository>();
			services.AddScoped<ITokenService,TokenService>();
			services.AddScoped<IOrderService,OrderService>();
			services.AddScoped<IResponseCacheService,ResponseCacheService>();

			var scope = services.BuildServiceProvider().CreateScope();

			var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
			context.Database.Migrate();

			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			SeedData(context, userManager);

			return services;
		}

		public static void SeedData(DatabaseContext context, UserManager<ApplicationUser> userManager)
		{
			var userSeeds = File.ReadAllText("../Infrastructure/Seeds/users.json");
			var users = JsonSerializer.Deserialize<List<ApplicationUser>>(userSeeds);

			if (!userManager.Users.Any())
			{
				foreach (var user in users)
				{
					userManager.CreateAsync(user, "password");
				}

			}

			var productTypeSeeds = File.ReadAllText("../Infrastructure/Seeds/types.json");
			var productTypeList = JsonSerializer.Deserialize<List<ProductType>>(productTypeSeeds);

			if (! context.ProductTypes.Any())
			{
				foreach (var productType in productTypeList)
				{
					context.ProductTypes.Add(productType);
				}

			}

			var productBrandSeeds = File.ReadAllText("../Infrastructure/Seeds/brands.json");
			var productBrandList = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandSeeds);

			if (!context.ProductBrands.Any())
			{
				foreach (var productBrand in productBrandList)
				{
					context.ProductBrands.Add(productBrand);
				}

			}

			var productSeeds = File.ReadAllText("../Infrastructure/Seeds/products.json");
			var productList = JsonSerializer.Deserialize<List<Product>>(productSeeds);

			if (!context.Products.Any())
			{
				foreach (var product in productList)
				{
					context.Products.Add(product);
				}

				context.SaveChanges();
			}
		}
	}
}
