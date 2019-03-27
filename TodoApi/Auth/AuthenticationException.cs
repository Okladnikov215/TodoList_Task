namespace TodoApi.Auth
{
    using System;

    public class AuthenticationException : Exception
    {
        public AuthenticationException()
            : base("Invalid credentials.")
        {
        }

        public AuthenticationException(string message) :
            base()
        {
        }
    }
}
