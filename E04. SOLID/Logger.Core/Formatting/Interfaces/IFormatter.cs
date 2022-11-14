namespace Logger.Core.Formatting.Interfaces
{
    using Layouts.Interfaces;
    using Models.Interfaces;

    public interface IFormatter
    {
        string Format(IMessage message, ILayout layout);
    }
}
