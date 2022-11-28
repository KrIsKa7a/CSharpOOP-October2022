namespace P01.PrototypePattern.Models
{
    using System;
    using System.Collections.Generic;

    public class Sandwich : SandwichPrototype
    {
        private string bread;
        private string meat;
        private string cheese;
        private string veggies;

        //Test the behaviour of mutable reference type
        private ICollection<string> addings;

        public Sandwich(string bread, string meat, string cheese, string veggies)
        {
            this.bread = bread;
            this.meat = meat;
            this.cheese = cheese;
            this.veggies = veggies;

            this.addings = new List<string>();
        }

        //Prototype pattern -> Shallow copy
        public override SandwichPrototype Clone()
        {
            //Info for the user, not part of the prototype pattern
            Console.WriteLine($"Cloning sandwich with ingridients: {this.GetIngridientsList()}");

            return this.MemberwiseClone() as SandwichPrototype;
        }

        public void AddIngridient(string name)
        {
            this.addings.Add(name);
        }

        //Info about current object
        private string GetIngridientsList()
            => $"{this.bread}, {this.meat}, {this.cheese}, {this.veggies}";
    }
}
