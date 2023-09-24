using Core.Entites.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.OwnsOne(o => o.ShipToAddress, a => a.WithOwner());
			builder.Property(o => o.OrderStatus).HasConversion(os => os.ToString(), os => (OrderStatus)Enum.Parse(typeof(OrderStatus), os));
			builder.HasMany(o => o.OrderedItems).WithOne().OnDelete(DeleteBehavior.Cascade);
		
		}
	}
}
