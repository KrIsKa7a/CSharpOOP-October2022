namespace CommandPattern.Utilities.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string cmdName, string[] args);
    }
}
