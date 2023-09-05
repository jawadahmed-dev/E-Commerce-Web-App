using System;

namespace Api.Exceptions
{

    public class UnprocessableRequestException : Exception
    {
        public UnprocessableRequestException(string message) : base(message) { }
    }
}
