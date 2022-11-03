//In Model classes we should not use Console or any other visualiser directly!
//It is preffered to return a string and another class should handle it
namespace Telephony.Models
{
    using System.Linq;

    using Interfaces;
    using Telephony.Exceptions;

    public class Smartphone : ISmartphone
    {
        public Smartphone()
        {

        }

        public string Call(string phoneNumber)
        {
            if (!this.ValidatePhoneNumber(phoneNumber))
            {
                throw new InvalidPhoneNumberException();
            }

            return $"Calling... {phoneNumber}";
        }

        public string BrowseURL(string url)
        {
            if (!this.ValidateURL(url))
            {
                throw new InvalidURLException();
            }

            return $"Browsing: {url}!";
        }

        private bool ValidatePhoneNumber(string phoneNumber)
            => phoneNumber.All(ch => char.IsDigit(ch));

        private bool ValidateURL(string url)
            => url.All(ch => !char.IsDigit(ch));
    }
}
