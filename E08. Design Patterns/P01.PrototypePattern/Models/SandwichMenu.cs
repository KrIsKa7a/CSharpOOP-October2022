namespace P01.PrototypePattern.Models
{
    using System.Collections.Generic;

    public class SandwichMenu
    {
        private IDictionary<string, SandwichPrototype> sandwiches;

        public SandwichMenu()
        {
            this.sandwiches = new Dictionary<string, SandwichPrototype>();
        }

        public SandwichPrototype this[string name]
        {
            get
            {
                return this.sandwiches[name];
            }
            set
            {
                this.sandwiches[name] = value;
            }
        } 
    }
}
