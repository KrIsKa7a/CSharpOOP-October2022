namespace Vehicles.Models
{
    using Exceptions;
    using Interfaces;

    public abstract class Vehicle : IVehicle
    {
        protected Vehicle(double fuelQuantity, double fuelConsumption, 
            double fuelConsumptionIncrement)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption + fuelConsumptionIncrement;
        }

        public double FuelQuantity { get; private set; }

        public double FuelConsumption { get; private set; }

        public string Drive(double distance)
        {
            double neededFuel = distance * this.FuelConsumption;
            if (neededFuel > this.FuelQuantity)
            {
                //Not enough fuel
                throw new InsufficientFuelException(string.Format(
                    ExceptionMessages.InsufficientFuelExceptionMessage, this.GetType().Name));
            }

            this.FuelQuantity -= neededFuel;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        //Truck has different refuel logic =>
        //We make the method virtual and Truck will override it (Run-Time Polymorphism)
        public virtual void Refuel(double liters)
        {
            this.FuelQuantity += liters;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.FuelQuantity:f2}";
        }
    }
}
