namespace Telephony.Exceptions
{
    using System;

    public class InvalidURLException : Exception
    {
        private const string DefaultMessage =
            "Invalid URL!";

        public InvalidURLException()
            : base(DefaultMessage)
        {

        }

        public InvalidURLException(string message)
            : base(message)
        {

        }
    }
}
