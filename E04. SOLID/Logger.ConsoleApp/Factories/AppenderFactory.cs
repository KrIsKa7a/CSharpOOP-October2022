namespace Logger.ConsoleApp.Factories
{
    using Logger.ConsoleApp.Factories.Interfaces;
    using Logger.Core.Appenders;
    using Logger.Core.Appenders.Interfaces;
    using Logger.Core.Enums;
    using Logger.Core.Formatting.Layouts.Interfaces;
    using Logger.Core.IO.Interfaces;

    public class AppenderFactory : IAppenderFactory
    {
        public IAppender CreateAppender(string type, ILayout layout, ReportLevel reportLevel = 0, ILogFile logFile = null)
        {
            IAppender appender;
            if (type == "ConsoleAppender")
            {
                appender = new ConsoleAppender(layout, reportLevel);
            }
            else if (type == "FileAppender")
            {
                appender = new FileAppender(layout, logFile, reportLevel);
            }
            else
            {
                throw new InvalidOperationException("Invalid appender type!");
            }

            return appender;
        }
    }
}
