namespace Logger.Core.Exceptions
{
    public class InvalidPathException : Exception
    {
        private const string DefaultMessage =
            "Provided path does not exist or it is invalid!";

        public InvalidPathException()
            : base(DefaultMessage)
        {

        }

        public InvalidPathException(string message) 
            : base(message)
        {

        }
    }
}
