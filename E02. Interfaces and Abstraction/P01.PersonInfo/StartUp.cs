namespace PersonInfo
{
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            string id = Console.ReadLine();
            string birthdate = Console.ReadLine();

            IIdentifiable identifiable = new Citizen(name, age, id, birthdate);
            IBirthable birthable = new Citizen(name, age, id, birthdate);

            Console.WriteLine(identifiable.Id);
            Console.WriteLine(birthable.Birthdate);

            //identifiable.Name; //ERROR, it is hidden behind the abstraction
            //birthable.Name //ERROR


            //person.SayMyName(); //ERROR
            //Citizen citizen = new Citizen(name, age);
            //citizen.SayMyName(); //VALID
        }
    }
}
