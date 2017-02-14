﻿using System;

namespace MyServiceLibrary.Exceptions
{
    public class ExistingUserException : Exception
    {
        public ExistingUserException()
        {
        }

        public ExistingUserException(string message) : base(message)
        {
        }

        public ExistingUserException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}