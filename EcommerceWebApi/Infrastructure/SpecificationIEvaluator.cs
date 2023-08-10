using Core.Entites.Common;
using Core.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public static class SpecificationIEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
		{
			var query = inputQuery;

			if (spec.Criteria != null) 
			{
				query.Where(spec.Criteria);
			}

			if (spec.Includes.Count > 0) 
			{
				query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
			}

			return query;
		}
	}
}
