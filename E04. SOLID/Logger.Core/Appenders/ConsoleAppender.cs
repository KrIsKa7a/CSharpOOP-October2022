namespace Logger.Core.Appenders
{
    using Appenders.Interfaces;
    using Enums;
    using Formatting;
    using Formatting.Layouts.Interfaces;
    using Formatting.Interfaces;
    using Models.Interfaces;

    public class ConsoleAppender : IAppender
    {
        private readonly IFormatter formatter;

        private ConsoleAppender()
        {
            this.formatter = new MessageFormatter();
        }

        public ConsoleAppender(ILayout layout, ReportLevel reportLevel = 0)
            : this()
        {
            this.Layout = layout;
            this.ReportLevel = reportLevel;
        }

        public ILayout Layout { get; private set; }

        public ReportLevel ReportLevel { get; private set; }

        public void AppendMessage(IMessage message)
        {
            string output = this.formatter.Format(message, this.Layout);
            Console.WriteLine(output);
        }
    }
}
