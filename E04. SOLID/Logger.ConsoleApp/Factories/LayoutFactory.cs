namespace Logger.ConsoleApp.Factories
{
    using Logger.ConsoleApp.CustomLayouts;
    using Logger.ConsoleApp.Factories.Interfaces;
    using Logger.Core.Formatting.Layouts;
    using Logger.Core.Formatting.Layouts.Interfaces;

    public class LayoutFactory : ILayoutFactory
    {
        public ILayout CreateLayout(string type)
        {
            ILayout layout;
            if (type == "SimpleLayout")
            {
                layout = new SimpleLayout();
            }
            else if (type == "XmlLayout")
            {
                layout = new XmlLayout();
            }
            else
            {
                throw new InvalidOperationException("Invalid layout type!");
            }

            return layout;
        }
    }
}
