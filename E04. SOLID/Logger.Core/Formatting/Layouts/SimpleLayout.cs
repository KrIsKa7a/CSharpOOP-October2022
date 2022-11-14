namespace Logger.Core.Formatting.Layouts
{
    using Interfaces;

    public class SimpleLayout : ILayout
    {
        public string Format
            => "{0} - {1} - {2}";
    }
}
