namespace Telephony.Exceptions
{
    using System;

    public class InvalidPhoneNumberException : Exception
    {
        private const string DefaultExceptionMessage =
            "Invalid number!";

        public InvalidPhoneNumberException()
            : base(DefaultExceptionMessage)
        {

        }

        public InvalidPhoneNumberException(string message)
            : base(message)
        {

        }
    }
}
