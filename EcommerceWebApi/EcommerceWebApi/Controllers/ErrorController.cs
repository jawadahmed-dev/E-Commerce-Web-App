using EcommerceWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
	
	
	[Route("errors/{code}")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : BaseApiController
	{
		public IActionResult Errors(int code) 
		{
			return new ObjectResult(Response<string>.Failure(code, null));
		}
	}
}
