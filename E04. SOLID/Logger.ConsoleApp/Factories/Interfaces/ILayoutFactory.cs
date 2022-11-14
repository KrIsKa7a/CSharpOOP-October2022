namespace Logger.ConsoleApp.Factories.Interfaces
{
    using Logger.Core.Formatting.Layouts.Interfaces;

    public interface ILayoutFactory
    {
        ILayout CreateLayout(string type);
    }
}
