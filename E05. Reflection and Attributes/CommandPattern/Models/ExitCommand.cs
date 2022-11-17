namespace CommandPattern.Models
{
    using System;

    using Contracts;

    public class ExitCommand : ICommand
    {
        private const int DefaultErrorCode = 0;

        public string Execute(string[] args)
        {
            Environment.Exit(DefaultErrorCode);
            return null;
        }
    }
}
