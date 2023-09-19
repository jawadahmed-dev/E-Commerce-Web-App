using Core.Entites;
using Core.Entites.OrderAggregate;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure
{
	public class DatabaseContext : IdentityDbContext<ApplicationUser>
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{

		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<ProductBrand> ProductBrands { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethod { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
