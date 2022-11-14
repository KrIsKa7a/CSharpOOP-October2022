namespace Logger.Core.Appenders
{
    using Enums;
    using Formatting;
    using Formatting.Interfaces;
    using Formatting.Layouts.Interfaces;
    using Interfaces;
    using IO.Interfaces;
    using Models.Interfaces;

    public class FileAppender : IAppender
    {
        private readonly IFormatter formatter;

        private FileAppender()
        {
            this.formatter = new MessageFormatter();
        }

        public FileAppender(ILayout layout, ILogFile logFile, ReportLevel reportLevel = 0)
            : this()
        {
            this.Layout = layout;
            this.LogFile = logFile;
            this.ReportLevel = reportLevel;
        }

        public ILayout Layout { get; private set; }

        public ILogFile LogFile { get; private set; }

        public ReportLevel ReportLevel { get; private set; }

        public void AppendMessage(IMessage message)
        {
            string output = this.formatter.Format(message, this.Layout);
            this.LogFile.WriteLine(output);
            File.AppendAllText(this.LogFile.Path + "/" + this.LogFile.Name, output + Environment.NewLine);
        }
    }
}
