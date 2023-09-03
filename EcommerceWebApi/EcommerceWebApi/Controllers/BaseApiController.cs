using EcommerceWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	[Consumes(MediaTypeNames.Application.Json)]
	public class BaseApiController : ControllerBase
	{

	}
}
