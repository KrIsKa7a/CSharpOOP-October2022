using NavalVessels.Models.Contracts;

namespace NavalVessels.Utilities.Factories.Contracts
{
    public interface IVesselFactory
    {
        IVessel Produce(string type, string name, double mainWeaponCaliber, double speed);
    }
}
