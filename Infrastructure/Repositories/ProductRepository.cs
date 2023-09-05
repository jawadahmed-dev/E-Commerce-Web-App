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

		public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
		{
			return await _databaseContext.ProductBrands.ToListAsync();
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _databaseContext.Products
				.AsSplitQuery()
				.AsNoTracking()
				.Include(p => p.ProductType)
				.Include(p => p.ProductBrand)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IReadOnlyList<Product>> GetProductsAsync()
		{
			return await _databaseContext.Products
				.AsSplitQuery()
				.AsNoTracking()
				.Include(p => p.ProductType)
				.Include(p => p.ProductBrand)
				.ToListAsync();
		}

		public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
		{
			return await _databaseContext.ProductTypes.ToListAsync();
		}
	}
}
