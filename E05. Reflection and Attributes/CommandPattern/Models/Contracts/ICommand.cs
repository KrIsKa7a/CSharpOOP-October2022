namespace CommandPattern.Models.Contracts
{
    public interface ICommand
    {
        string Execute(string[] args);
    }
}
