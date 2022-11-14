namespace Logger.Core.Utilities
{
    public static class DateTimeValidator
    {
        public static bool IsDateTimeValid(string dateTime)
        {
            return DateTime.TryParse(dateTime, out DateTime dateTimeRes);
        }
    }
}
