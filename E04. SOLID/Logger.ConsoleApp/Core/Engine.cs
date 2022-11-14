namespace Logger.ConsoleApp.Core
{
    using System;

    using Logger.ConsoleApp.Core.Interfaces;
    using Logger.ConsoleApp.Factories;
    using Logger.ConsoleApp.Factories.Interfaces;
    using Logger.Core.Appenders.Interfaces;
    using Logger.Core.Enums;
    using Logger.Core.Formatting.Layouts.Interfaces;
    using Logger.Core.IO;
    using Logger.Core.IO.Interfaces;
    using Logger.Core.Logging;
    using Logger.Core.Logging.Interfaces;

    public class Engine : IEngine
    {
        private readonly ICollection<IAppender> appenders;
        private ILogger logger;

        private readonly ILayoutFactory layoutFactory;
        private readonly IAppenderFactory appenderFactory;

        public Engine()
        {
            this.appenders = new HashSet<IAppender>();

            this.layoutFactory = new LayoutFactory();
            this.appenderFactory = new AppenderFactory();
        }

        public void Run()
        {
            this.CreateAppenders();

            this.logger = new Logger(this.appenders);

            this.LogMessages();
        }

        private void CreateAppenders()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                try
                {
                    string[] appenderArgs = Console.ReadLine()
                    .Split(' ');
                    string appenderType = appenderArgs[0];
                    string layoutType = appenderArgs[1];

                    ILayout layout = this.layoutFactory.CreateLayout(layoutType);
                    ReportLevel reportLevel = ReportLevel.Info;
                    if (appenderArgs.Length == 3)
                    {
                        bool isReportLevelValid = Enum.TryParse<ReportLevel>(appenderArgs[2], true, out reportLevel);
                        if (!isReportLevelValid)
                        {
                            throw new InvalidOperationException("Report level is not valid!");
                        }
                    }

                    IAppender appender;
                    if (appenderType == "FileAppender")
                    {
                        ILogFile logFile = new LogFile("log.xml", "../../../Logs");
                        appender = this.appenderFactory.CreateAppender(appenderType, layout, reportLevel, logFile);
                    }
                    else
                    {
                        appender = this.appenderFactory.CreateAppender(appenderType, layout, reportLevel);
                    }

                    this.appenders.Add(appender);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void LogMessages()
        {
            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] cmdArgs = command
                    .Split("|");
                string reportLevelStr = cmdArgs[0];
                string dateTime = cmdArgs[1];
                string message = cmdArgs[2];

                try
                {
                    if (reportLevelStr == "INFO")
                    {
                        this.logger.Info(dateTime, message);
                    }
                    else if (reportLevelStr == "WARNING")
                    {
                        this.logger.Warn(dateTime, message);
                    }
                    else if (reportLevelStr == "ERROR")
                    {
                        this.logger.Error(dateTime, message);
                    }
                    else if (reportLevelStr == "CRITICAL")
                    {
                        this.logger.Critical(dateTime, message);
                    }
                    else if (reportLevelStr == "FATAL")
                    {
                        this.logger.Fatal(dateTime, message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
