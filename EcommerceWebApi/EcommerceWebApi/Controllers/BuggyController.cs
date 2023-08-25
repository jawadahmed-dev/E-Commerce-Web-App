using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]
	public class BuggyController : BaseApiController
	{
		[HttpGet("ServerError")]
		public IActionResult GetServerError() 
		{
			throw new NullReferenceException();
		}

		[HttpGet("ValidationError/{id}")]
		public IActionResult GetValidationError(int id)
		{
			return new BadRequestObjectResult("bad request");
		}

		[HttpGet("BadReqeustError")]
		public IActionResult GetBadReqeustError()
		{
			return BadRequest("bad request");
		}

	}
}
