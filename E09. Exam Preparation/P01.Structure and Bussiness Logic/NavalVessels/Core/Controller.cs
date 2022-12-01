namespace NavalVessels.Core
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;
    using Models.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;

    public class Controller : IController
    {
        private const double UnarmoredVesselArmorThickness = 0;

        private readonly IRepository<IVessel> vessels;
        private readonly ICollection<ICaptain> captains;

        //private readonly IVesselFactory vesselFactory;

        public Controller()
        {
            this.vessels = new VesselRepository();
            this.captains = new HashSet<ICaptain>();

            //this.vesselFactory = new VesselFactory();
        }

        public string HireCaptain(string fullName)
        {
            ICaptain captain = new Captain(fullName);

            if (this.captains.Any(c => c.FullName == fullName))
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }

            this.captains.Add(captain);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            IVessel existingVessel = this.vessels.FindByName(name);
            if (existingVessel != null)
            {
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, existingVessel.GetType().Name, name);
            }

            //In order to work with Judge!
            //try
            //{
            //    IVessel producedVessel = this.vesselFactory.Produce(vesselType, name, mainWeaponCaliber, speed);
            //    this.vessels.Add(producedVessel);
            //    return string.Format(OutputMessages.SuccessfullyCreateVessel,
            //        vesselType, name, mainWeaponCaliber, speed);
            //}
            //catch (InvalidOperationException ioe)
            //{
            //    return ioe.Message;
            //}
            //catch (Exception e)
            //{
            //    throw e.InnerException;
            //}

            IVessel producedVessel;
            if (vesselType == "Battleship")
            {
                producedVessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Submarine")
            {
                producedVessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else
            {
                return OutputMessages.InvalidVesselType;
            }

            this.vessels.Add(producedVessel);
            return string.Format(OutputMessages.SuccessfullyCreateVessel,
                vesselType, name, mainWeaponCaliber, speed);
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain captainToAssign = this.captains
                .FirstOrDefault(c => c.FullName == selectedCaptainName);
            if (captainToAssign == null)
            {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }

            IVessel vessel = this.vessels
                .FindByName(selectedVesselName);
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }

            if (vessel.Captain != null)
            {
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }

            vessel.Captain = captainToAssign;
            captainToAssign.AddVessel(vessel);

            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        //Be causious: Captain may not exist
        public string CaptainReport(string captainFullName)
        {
            ICaptain captain = this.captains
                .First(c => c.FullName == captainFullName);

            return captain.Report();
        }

        //Be causious: Vessel may not exist
        public string VesselReport(string vesselName)
        {
            IVessel vessel = this.vessels
                .FindByName(vesselName);

            return vessel?.ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = this.vessels
                .FindByName(vesselName);
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }

            if (vessel.GetType() == typeof(Battleship))
            {
                Battleship battleshipVessel = (Battleship)vessel;
                battleshipVessel.ToggleSonarMode();

                return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
            }
            else
            {
                Submarine submarineVessel = (Submarine)vessel;
                submarineVessel.ToggleSubmergeMode();

                return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
            }
        }

        public string ServiceVessel(string vesselName)
        {
            IVessel vessel = this.vessels
                .FindByName(vesselName);
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }

            vessel.RepairVessel();
            return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            IVessel attackingVessel = this.vessels
                .FindByName(attackingVesselName);
            if (attackingVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }

            IVessel defendingVessel = this.vessels
                .FindByName(defendingVesselName);
            if (defendingVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, defendingVesselName);
            }

            if (attackingVessel.ArmorThickness == UnarmoredVesselArmorThickness)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }
            if (defendingVessel.ArmorThickness == UnarmoredVesselArmorThickness)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);
            }

            attackingVessel.Attack(defendingVessel);
            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defendingVessel.ArmorThickness);
        }
    }
}
