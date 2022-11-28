namespace P03.TemplateMethodPattern.Models
{
    using System;

    public class TwelveGrain : Bread
    {
        public override void MixIngridients()
        {
            Console.WriteLine($"Gathering ingredients for 12-Grain Bread.");
        }
        public override void Bake()
        {
            Console.WriteLine($"Baking the 12-Grain Bread. (25 minutes left)");
        }
    }
}
