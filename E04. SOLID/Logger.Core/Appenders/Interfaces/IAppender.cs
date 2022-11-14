namespace Logger.Core.Appenders.Interfaces
{
    using Enums;
    using Formatting.Layouts.Interfaces;
    using Models.Interfaces;

    public interface IAppender
    {
        ILayout Layout { get; }

        ReportLevel ReportLevel { get; }

        void AppendMessage(IMessage message);
    }
}
