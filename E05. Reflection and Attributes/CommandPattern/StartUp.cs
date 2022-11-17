namespace CommandPattern
{
    using CommandPattern.Core.Contracts;
    using CommandPattern.Utilities.Contracts;
    using Core;
    using Utilities;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            ICommandInterpreter command = new CommandInterpreter();
            IEngine engine = new Engine(command);
            engine.Run();
        }
    }
}
