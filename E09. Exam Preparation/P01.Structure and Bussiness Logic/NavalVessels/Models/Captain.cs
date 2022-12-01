namespace NavalVessels.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Contracts;
    using Utilities.Messages;

    public class Captain : ICaptain
    {
        private const int CombatExperienceIncreaseStep = 10;

        private string fullName;

        private Captain()
        {
            this.CombatExperience = 0;
            this.Vessels = new HashSet<IVessel>();
        }

        public Captain(string fullName)
            : this()
        {
            this.FullName = fullName;
        }

        public string FullName
        {
            get
            {
                return this.fullName;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);
                }

                this.fullName = value;
            }
        }

        public int CombatExperience { get; private set; }

        public ICollection<IVessel> Vessels { get; private set; }

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);
            }

            this.Vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            this.CombatExperience += CombatExperienceIncreaseStep;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine($"{this.FullName} has {this.CombatExperience} combat experience and commands {this.Vessels.Count} vessels.");

            foreach (IVessel vessel in this.Vessels)
            {
                sb
                    .AppendLine(vessel.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
