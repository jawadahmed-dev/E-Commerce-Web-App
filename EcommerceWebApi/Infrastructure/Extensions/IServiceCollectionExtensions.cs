using Core.Entites;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
		public async static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
		{
			
			
			services.AddDbContext<DatabaseContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("Default"));
			});

			services.AddScoped<IProductRepository, ProductRepository>();

			var scope = services.BuildServiceProvider().CreateScope();

			var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
			await context.Database.MigrateAsync();

			await SeedData(context);

		}

		public async static Task SeedData(DatabaseContext context)
		{
			var productTypeSeeds = File.ReadAllText("../Infrastructure/Seeds/ProductTypeSeed.json");
			var productTypeList = JsonSerializer.Deserialize<List<ProductType>>(productTypeSeeds);

			if (! context.ProductTypes.Any())
			{
				foreach (var productType in productTypeList)
				{
					context.ProductTypes.Add(productType);
				}

				await context.SaveChangesAsync();
			}

			var productBrandSeeds = File.ReadAllText("../Infrastructure/Seeds/ProductBrandSeed.json");
			var productBrandList = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandSeeds);

			if (!context.ProductBrands.Any())
			{
				foreach (var productBrand in productBrandList)
				{
					context.ProductBrands.Add(productBrand);
				}

				await context.SaveChangesAsync();
			}

			var productSeeds = File.ReadAllText("../Infrastructure/Seeds/ProductSeed.json");
			var productList = JsonSerializer.Deserialize<List<Product>>(productSeeds);

			if (! context.Products.Any())
			{
				foreach (var product in productList)
				{
					context.Products.Add(product);
				}

				await context.SaveChangesAsync();
			}
		}
	}
}
