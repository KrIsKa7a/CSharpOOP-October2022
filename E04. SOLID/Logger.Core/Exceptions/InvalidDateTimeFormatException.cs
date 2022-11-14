namespace Logger.Core.Exceptions
{
    public class InvalidDateTimeFormatException : Exception
    {
        private const string DefaultMessage = 
            "Provided DateTime format not supported! Try register it using Validator.RegisterNewFormat() method!";

        public InvalidDateTimeFormatException()
            : base(DefaultMessage)
        {

        }

        public InvalidDateTimeFormatException(string message)
            : base(message)
        {

        }
    }
}
