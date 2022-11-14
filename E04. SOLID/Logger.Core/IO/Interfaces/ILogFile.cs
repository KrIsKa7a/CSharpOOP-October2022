namespace Logger.Core.IO.Interfaces
{
    public interface ILogFile
    {
        string Name { get; }

        string Path { get; }

        string FullPath { get; }

        string Content { get; }

        int Size { get; }

        void Write(string text);

        void WriteLine(string text);
    }
}
