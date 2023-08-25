using Core.Entites.Common;
using Core.Interfaces.Repositories;
using Core.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly DatabaseContext _context;

		public GenericRepository(DatabaseContext context)
		{
			_context = context;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}

		public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
		{
			return await ApplySpecifications(spec).AsSplitQuery().ToListAsync();
		}
		public async Task<int> CountAsync(ISpecification<T> spec)
		{
			return await ApplySpecifications(spec).CountAsync();
		}

		private IQueryable<T> ApplySpecifications(ISpecification<T> spec) 
		{
			return SpecificationIEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
		}

	}
}
