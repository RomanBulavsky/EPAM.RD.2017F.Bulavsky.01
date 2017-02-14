using System;

namespace MyServiceLibrary.Exceptions
{
    public class NotValidUserException : Exception
    {
        public NotValidUserException()
        {
        }

        public NotValidUserException(string message) : base(message)
        {
        }

        public NotValidUserException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}