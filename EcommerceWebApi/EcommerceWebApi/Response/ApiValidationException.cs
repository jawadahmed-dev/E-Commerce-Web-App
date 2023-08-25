using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Response
{
	public class ApiValidationException : ApiResponse
	{
		public ApiValidationException(IEnumerable<string> errors = null) : base(400)
		{
			Errors = errors;
		}
		public IEnumerable<string> Errors { get; set; }
	}
}
