namespace Logger.Core.Formatting
{
    using Interfaces;
    using Layouts.Interfaces;
    using Models.Interfaces;

    public class MessageFormatter : IFormatter
    {
        public string Format(IMessage message, ILayout layout)
        {
            return string.Format(layout.Format, message.DateTime, message.ReportLevel.ToString(), message.MessageText);
        }
    }
}
