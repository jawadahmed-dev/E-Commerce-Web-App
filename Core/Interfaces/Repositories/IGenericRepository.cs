using Core.Entites.Common;
using Core.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		public Task<T> GetByIdAsync(int id);
		public Task<IReadOnlyList<T>> ListAllAsync();
		public Task<T> GetEntityWithSpec(ISpecification<T> spec);
		public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
		public Task<int> CountAsync(ISpecification<T> spec);
	}
}
