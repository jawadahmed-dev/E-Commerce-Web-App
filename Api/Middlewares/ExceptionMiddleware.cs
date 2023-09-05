using Api.Exceptions;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext context) 
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex) 
			{
				_logger.LogError(ex.Message);

				var code = StatusCodes.Status500InternalServerError;
				var errors = new List<string> { ex.Message };

				code = ex switch
				{
					NotFoundException => StatusCodes.Status404NotFound,
					BadRequestException => StatusCodes.Status400BadRequest,
					UnprocessableRequestException => StatusCodes.Status422UnprocessableEntity,
					_ => code
				};

				var result = JsonSerializer.Serialize(Response<string>.Failure(code, errors));

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = code;

				await context.Response.WriteAsync(result);
			}
		}
	}
}
