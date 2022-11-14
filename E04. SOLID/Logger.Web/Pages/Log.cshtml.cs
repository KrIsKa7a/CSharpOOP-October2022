using Logger.Core.Formatting;
using Logger.Core.Formatting.Interfaces;
using Logger.Core.Formatting.Layouts;
using Logger.Core.Models;
using Logger.Core.Models.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Logger.Web.Pages
{
    public class LogModel : PageModel
    {
        public void OnGet()
        {
            var simpleLayout = new SimpleLayout();
            IFormatter formatter = new MessageFormatter();
            IMessage message = new Message("We are on the Web!", DateTime.Now.ToString("G"), Core.Enums.ReportLevel.Info);

            string output = formatter.Format(message, simpleLayout);
            this.DisplayMessage = output;
        }

        public string DisplayMessage { get; set; }
    }
}
