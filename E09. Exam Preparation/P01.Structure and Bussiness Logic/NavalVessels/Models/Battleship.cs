namespace NavalVessels.Models
{
    using System.Text;

    using Contracts;

    public class Battleship : Vessel, IBattleship
    {
        private const double InitialArmorThickness = 300;
        private const double MainWeaponCaliberChange = 40;
        private const double SpeedChange = 5;

        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, InitialArmorThickness)
        {
            this.SonarMode = false;
        }

        public bool SonarMode { get; private set; }

        public void ToggleSonarMode()
        {
            if (!this.SonarMode)
            {
                this.MainWeaponCaliber += MainWeaponCaliberChange;
                this.Speed -= SpeedChange;
            }
            else
            {
                this.MainWeaponCaliber -= MainWeaponCaliberChange;
                this.Speed += SpeedChange;
            }

            //Flip mode -> Toggle
            //False -> True
            //True -> False
            this.SonarMode = !this.SonarMode;
        }

        public override void RepairVessel()
        {
            this.ArmorThickness = InitialArmorThickness;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string sonarModeOnOff = this.SonarMode ? "ON" : "OFF";

            sb
                .AppendLine(base.ToString())
                .AppendLine($" *Sonar mode: {sonarModeOnOff}");
            return sb.ToString().TrimEnd();
        }
    }
}
