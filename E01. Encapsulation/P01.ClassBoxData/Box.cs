//private -> visible only in the current class, least visual (higher encapsulation)
//protected -> visiuble only in the inherited members
//internal -> visible only in the current assembly
//public -> visible in the whole solution (most visual, lowest encapsulation)

//Fields -> private
//Ctors -> private (ctor chaining), protected (inheritance), public
//Properties -> public (get, set -> private, protected)
//Methods -> private, protected, public

//Validation -> get (just return the value of a field), set (validate the value), methods (additional validations)
namespace P01.ClassBoxData
{
    using System;
    using System.Text;

    public class Box
    {
        //Manual backing fields
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        {
            get
            {
                return this.length;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(
                        ExceptionMessages.BoxParameterCannotBeZeroOrNegative, nameof(this.Length)));
                }

                this.length = value;
            }
        }

        public double Width
        {
            get
            {
                return this.width;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(
                        ExceptionMessages.BoxParameterCannotBeZeroOrNegative, nameof(this.Width)));
                }

                this.width = value;
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(
                        ExceptionMessages.BoxParameterCannotBeZeroOrNegative, nameof(this.Height)));
                }

                this.height = value;
            }
        }

        public double SurfaceArea()
            => this.LateralSurfaceArea() + (2 * this.Length * this.Width);


        public double LateralSurfaceArea()
            => (2 * this.Length * this.Height) + (2 * this.Width * this.Height);


        public double Volume()
            => this.Length * this.Width * this.Height;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine($"Surface Area - {this.SurfaceArea():f2}")
                .AppendLine($"Lateral Surface Area - {this.LateralSurfaceArea():f2}")
                .AppendLine($"Volume - {this.Volume():f2}");

            return sb.ToString().TrimEnd();
        }
    }
}
