using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
	public class Response<T> where T : class
	{
		public Response(){}

		public Response(int statusCode, T result, IEnumerable<string> errors)
		{
			StatusCode = statusCode;
			Message = GetStatusMessage(statusCode);
			Result = result;
			Errors = errors;
		}


		public int StatusCode { get; set; }
		public string Message { get; set; }
		public T Result { get; set; } = null;
		public IEnumerable<string> Errors { get; set; }

		private string GetStatusMessage(int statusCode)
		{
			return statusCode switch
			{
				200 => "Request Succeeded",
				201 => "Record Created Successfully",
				400 => "A bad request, you have made",
				401 => "Authorized, you are not",
				404 => "Resource found, it was not",
				500 => "Server Error, it is",
				_ => null
			};
		}

		public static Response<T> Success(int statusCode, T data) 
		{
			return new Response<T>(statusCode, data, null);
		}

		public static Response<T> Failure(int statusCode, IEnumerable<string> errors)
		{
			return new Response<T>(statusCode, default, errors);
		}

	}
}
