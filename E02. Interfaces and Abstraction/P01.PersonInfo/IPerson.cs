//Apart from the interface which describes our public part of the class, we want high level of enapsulation!
//High Abstraction + High Encapsulation

namespace PersonInfo
{
    public interface IPerson
    {
        string Name { get; }

        int Age { get; }
    }
}
