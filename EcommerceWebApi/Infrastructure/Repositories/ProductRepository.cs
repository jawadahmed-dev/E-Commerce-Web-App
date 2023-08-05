using Core.Entites;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly DatabaseContext _databaseContext;

		public ProductRepository(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _databaseContext.Products.FindAsync(id);
		}

		public async Task<IReadOnlyList<Product>> GetProductsAsync()
		{
			return await _databaseContext.Products.ToListAsync();
		}
	}
}
