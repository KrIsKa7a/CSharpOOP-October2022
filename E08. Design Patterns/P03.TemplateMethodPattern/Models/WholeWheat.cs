namespace P03.TemplateMethodPattern.Models
{
    using System;

    public class WholeWheat : Bread
    {
        public override void MixIngridients()
        {
            Console.WriteLine("Gathering ingredients for WholeWheat Bread.");
        }
        public override void Bake()
        {
            Console.WriteLine("Baking the Wholewheat Bread. (15 minutes)");
        }
    }
}
