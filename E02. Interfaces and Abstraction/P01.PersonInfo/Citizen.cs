namespace PersonInfo
{
    //Built-in libraries namespaces (System namespaces)
    using System;

    //Namespaces of our projects

    //Namespaces of installed libraries and frameworks

    //using interface we achieve multiple implementation (multiple inheritance)
    public class Citizen : IPerson, IIdentifiable, IBirthable
    {
        private string name;
        private int age;

        public Citizen(string name, int age, string id, string birthdate)
        {
            //When we have full properties with validation we always call the properties in the constructor, not the fields!
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Birthdate = birthdate;
        }

        //We write validation just to exercise (it's not neccessary)
        //We use auto-properties just to save some time for harder tasks
        //In real apps usually Id is on the top of the properties
        public string Id { get; private set; }

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Null cannot be null or whitespace!");
                }

                name = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Age must be a positive number!");
                }

                age = value;
            }
        }

        public string Birthdate { get; private set; }


        public void SayMyName()
        {
            Console.WriteLine($"Hello, I am {Name}!");
        }
    }
}
