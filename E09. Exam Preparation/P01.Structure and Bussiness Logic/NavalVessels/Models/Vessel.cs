namespace NavalVessels.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Utilities.Messages;

    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;

        private Vessel()
        {
            this.Targets = new List<string>();
        }

        //Judge may say X
        protected Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
            : this()
        {
            this.Name = name;
            this.MainWeaponCaliber= mainWeaponCaliber;
            this.Speed = speed;
            this.ArmorThickness = armorThickness;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.Name), ExceptionMessages.InvalidVesselName);
                }

                this.name = value;
            }
        }

        public ICaptain Captain 
        { 
            get
            {
                return this.captain;
            } 
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCaptainToVessel);
                }

                this.captain = value;
            }
        }

        public double ArmorThickness { get; set; }

        public double MainWeaponCaliber { get; protected set; }

        public double Speed { get; protected set; }

        public ICollection<string> Targets { get; private set; }

        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);
            }

            target.ArmorThickness -= this.MainWeaponCaliber;
            if (target.ArmorThickness < 0)
            {
                target.ArmorThickness = 0;
            }

            this.Targets.Add(target.Name);

            this.Captain.IncreaseCombatExperience();
            target.Captain.IncreaseCombatExperience();
        }

        public abstract void RepairVessel();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string targetsOutput = this.Targets.Any() ?
                String.Join(", ", this.Targets) : "None";

            sb
                .AppendLine($"- {this.Name}")
                .AppendLine($" *Type: {this.GetType().Name}")
                .AppendLine($" *Armor thickness: {this.ArmorThickness}")
                .AppendLine($" *Main weapon caliber: {this.MainWeaponCaliber}")
                .AppendLine($" *Speed: {this.Speed} knots")
                .AppendLine($" *Targets: {targetsOutput}");
            return sb.ToString().TrimEnd();
        }
    }
}
