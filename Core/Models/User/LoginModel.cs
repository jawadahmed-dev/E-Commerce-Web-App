using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.User
{
	public class LoginModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public class LoginResponseModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
	}
}
