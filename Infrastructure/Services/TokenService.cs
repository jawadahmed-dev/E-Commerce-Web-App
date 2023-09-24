using Core.Interfaces.Services;
using Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;
		private readonly SymmetricSecurityKey _securityKey;

		public TokenService(IConfiguration config)
		{
			_config = config;
			_securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
		}

		public string CreateToken(object applicationUser)
		{
			var user = applicationUser as ApplicationUser;

			var claims = new List<Claim> 
			{
				new Claim(ClaimTypes.Email, user.Email),
			};

			var signingCreds = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature);

			var tokenDesc = new SecurityTokenDescriptor 
			{
				Subject = new ClaimsIdentity(claims),
				Issuer = _config["Jwt:Issuer"],
				IssuedAt = DateTime.UtcNow,
				Audience = _config["Jwt:Audience"],
				Expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpirationInMinutes"])),
				SigningCredentials = signingCreds
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDesc);

			return tokenHandler.WriteToken(token);
		}
	}
}
