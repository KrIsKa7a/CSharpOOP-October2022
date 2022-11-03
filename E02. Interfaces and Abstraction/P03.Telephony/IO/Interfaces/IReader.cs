namespace Telephony.IO.Interfaces
{
    public interface IReader
    {
        //This RaedLine() is abstract and it is not neccessary reffered to the Console!!!
        //This can be Console.ReadLine() but also can be File.ReadLine()
        string ReadLine();
    }
}
