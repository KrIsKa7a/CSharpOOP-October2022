namespace Telephony.Models.Interfaces
{
    public interface ISmartphone : IStationaryPhone
    {
        string BrowseURL(string url);
    }
}
