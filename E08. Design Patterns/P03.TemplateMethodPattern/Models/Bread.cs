namespace P03.TemplateMethodPattern.Models
{
    using System;

    public abstract class Bread
    {
        public abstract void MixIngridients();

        public abstract void Bake();

        public virtual void Slice()
        {
            Console.WriteLine($"Slicing the {this.GetType().Name} bread!");
        }

        //Template method pattern -> Define order of calling of the abstract methods
        public void Make()
        {
            this.MixIngridients(); //I do not how will be implemented but it must be first
            this.Bake();
            this.Slice();
        }
    }
}
