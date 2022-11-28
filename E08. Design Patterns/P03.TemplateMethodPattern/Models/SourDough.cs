namespace P03.TemplateMethodPattern.Models
{
    using System;

    public class SourDough : Bread
    {
        public override void MixIngridients()
        {
            Console.WriteLine($"Gathering ingredients for Sourdough Bread.");
        }
        public override void Bake()
        {
            Console.WriteLine($"Baking the Sourdough Bread. (20 minutes)");
        }
    }
}
