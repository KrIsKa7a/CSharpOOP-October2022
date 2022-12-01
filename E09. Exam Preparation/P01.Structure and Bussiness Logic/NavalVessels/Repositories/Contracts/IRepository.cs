namespace NavalVessels.Repositories.Contracts
{
    using System.Collections.Generic;

    //IRepository<T>
    public interface IRepository<T>
    {
        //Store data about all created entities (models)
        IReadOnlyCollection<T> Models { get; }

        //Create
        void Add(T model);

        //Delete
        bool Remove(T model);

        //Read
        T FindByName(string name);
    }
}
