namespace Logger.Core.Models
{
    using Enums;
    using Exceptions;
    using Interfaces;
    using Utilities;

    public class Message : IMessage
    {
        private string? messageText;
        private string? dateTime;

        public Message(string messageText, string dateTime, ReportLevel reportLevel)
        {
            this.MessageText = messageText;
            this.DateTime = dateTime;
            this.ReportLevel = reportLevel;
        }

        public string MessageText 
        { 
            get
            {
                return this.messageText!;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new EmptyMessageTextException();
                }

                this.messageText = value;
            } 
        }

        public string DateTime 
        {
            get
            {
                return this.dateTime!;
            } 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new EmptyDateTimeException();
                }

                if (!DateTimeValidator.IsDateTimeValid(value))
                {
                    throw new InvalidDateTimeFormatException();
                }

                this.dateTime = value;
            } 
        }

        public ReportLevel ReportLevel { get; private set; }
    }
}
