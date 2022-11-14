namespace Logger.ConsoleApp
{
    using Logger.ConsoleApp.Core.Interfaces;
    using Logger.ConsoleApp.Core;

    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}