using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string GetEmailFromPrincipal(this ClaimsPrincipal claimsPrincipal) 
		{
			return claimsPrincipal?.FindFirstValue(ClaimTypes.Email);
		}
	}
}
