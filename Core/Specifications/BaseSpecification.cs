using Core.Entites.Common;
using Core.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; }

		public List<Expression<Func<T, object>>> Includes { get; } =
			new List<Expression<Func<T, object>>>();

		public Expression<Func<T, object>> OrderBy { get; set; }

		public Expression<Func<T, object>> OrderByDesc { get; set; }

		public int Take { get; set; }

		public int Skip { get; set; }

		public bool IsPagingEnabled { get; set; }

		public BaseSpecification()
		{
		}

		public BaseSpecification(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}

		public void AddInclude(Expression<Func<T, object>> include) 
		{
			Includes.Add(include);
		}

		public void AddOrderBy(Expression<Func<T, object>> orderBy)
		{
			OrderBy = orderBy;
		}

		public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
		{
			OrderByDesc = orderByDesc;
		}

		public void ApplyPaging(int take, int skip) 
		{
			Take = take;
			Skip = skip;
			IsPagingEnabled = true;

		}
	}
}
