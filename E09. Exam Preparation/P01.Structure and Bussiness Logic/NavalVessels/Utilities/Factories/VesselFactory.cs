namespace NavalVessels.Utilities.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Contracts;
    using Models.Contracts;
    using Utilities.Messages;

    public class VesselFactory : IVesselFactory
    {
        public IVessel Produce(string type, string name, double mainWeaponCaliber, double speed)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Type wantedType = assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            if (wantedType == null)
            {
                throw new InvalidOperationException(OutputMessages.InvalidVesselType);
            }

            IVessel instance = (IVessel) Activator
                .CreateInstance(wantedType, new object[] { name, mainWeaponCaliber, speed });
            return instance;
        }
    }
}
