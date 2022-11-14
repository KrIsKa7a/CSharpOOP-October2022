namespace Logger.ConsoleApp.CustomLayouts
{
    using System.Text;

    using Logger.Core.Formatting.Layouts.Interfaces;

    public class XmlLayout : ILayout
    {
        public string Format
            => this.CreateFormat();

        private string CreateFormat()
        {
            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine("<log>")
                .AppendLine("\t<date>{0}</date>")
                .AppendLine("\t<level>{1}</level>")
                .AppendLine("\t<message>{2}</message>")
                .AppendLine("</log>");

            return sb.ToString().TrimEnd();
        }
    }
}
