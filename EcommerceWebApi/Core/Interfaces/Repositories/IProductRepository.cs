using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface IProductRepository
	{
		public Task<Product> GetProductByIdAsync(int id);
		public Task<IReadOnlyList<Product>> GetProductsAsync();
	}
}
