namespace Logger.Core.Models.Interfaces
{
    using Enums;

    public interface IMessage
    {
        string MessageText { get; }

        string DateTime { get; }

        ReportLevel ReportLevel { get; }
    }
}
