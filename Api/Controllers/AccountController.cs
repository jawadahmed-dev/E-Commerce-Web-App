using Api.Exceptions;
using Api.Models;
using Core.Interfaces.Services;
using Core.Models.User;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly ITokenService _tokenService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(ITokenService tokenService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_tokenService = tokenService;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[Authorize]
		[HttpGet("load-user")]
		public async Task<ActionResult<Response<UserResponseModel>>> LoadUserAsync() 
		{
			var email = User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email)?.Value;

			if (string.IsNullOrEmpty(email)) throw new BadRequestException("User doesn't contain email claim.");

			var user = await _userManager.FindByEmailAsync(email);

			if (user == null) throw new NotFoundException("User coudn't be found with this email.");

			return Response<UserResponseModel>.Success(200,
				new UserResponseModel
				{
					Name = user.DisplayName,
					Email = email,
					Token = _tokenService.CreateToken(user)
				});

		}

		[HttpPost("login")]
		public async Task<ActionResult<Response<LoginResponseModel>>> LoginAsync(LoginModel loginUserModel)
		{ 
			var user = await _userManager.FindByEmailAsync(loginUserModel.Email);

			if (user == null) throw new NotFoundException("User coudn't be found with this email.");

			var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginUserModel.Password, false);

			if (!signInResult.Succeeded) throw new BadRequestException("Password is not correct.");
			
			return Response<LoginResponseModel>.Success(200, 
				new LoginResponseModel 
				{
					Name = user.DisplayName,
					Email = loginUserModel.Email,
					Token = _tokenService.CreateToken(user) 
				});
		}

		[HttpGet("check-email-exists")]
		public async Task<ActionResult<Response<bool>>> CheckEmailExistAsync(string email) 
		{
			return Response<bool>.Success(200, await _userManager.FindByEmailAsync(email) != null);
		}
	}
}
