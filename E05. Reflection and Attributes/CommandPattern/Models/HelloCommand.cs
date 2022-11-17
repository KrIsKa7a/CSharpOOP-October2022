namespace CommandPattern.Models
{
    using Contracts;

    public class HelloCommand : ICommand
    {
        public string Execute(string[] args)
        {
            //Very simple example
            //Very complex code
            return $"Hello, {args[0]}";
        }
    }
}
