using Core.Interfaces.Repositories;
using Api.Middlewares;
using Api.Response;
using Infrastructure;
using Infrastructure.Extensions;
using Api.Extensions;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(o => 
			{
				o.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
					ValidateIssuerSigningKey = true,
					ValidIssuer = Configuration["Jwt:Issuer"],
					ValidateIssuer = true,
					ValidAudience = Configuration["Jwt:Audience"],
					ValidateAudience = true,
					ValidateLifetime = true
				};
			});
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAnyOrigin",
					builder =>
					{
						builder.AllowAnyOrigin()
							   .AllowAnyHeader()
							   .AllowAnyMethod();
					});
			});
			services.AddInfrastructure(Configuration).AddApiServices();


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			app.UseCors("AllowAnyOrigin");

			app.UseMiddleware<ExceptionMiddleware>();

			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			if (env.IsDevelopment())
			{
				app.AddSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
