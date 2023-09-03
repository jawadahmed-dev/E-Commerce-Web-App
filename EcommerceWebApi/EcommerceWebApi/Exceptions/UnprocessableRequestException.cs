using System;

namespace EcommerceWebApi.Exceptions
{

    public class UnprocessableRequestException : Exception
    {
        public UnprocessableRequestException(string message) : base(message) { }
    }
}
