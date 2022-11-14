namespace Logger.Core.IO
{
    using System.Text;

    using Exceptions;
    using Interfaces;
    using Utilities;

    //Memory not really well optimized
    public class LogFile : ILogFile
    {
        private string? name;
        private string? path;

        private readonly StringBuilder content;

        private LogFile()
        {
            this.content = new StringBuilder();
        }

        public LogFile(string name, string path)
            : this()
        {
            this.Name = name;
            this.Path = path;
        }

        public string Name
        {
            get
            {
                return this.name!;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EmptyFileNameException();
                }

                this.name = value;
            }
        }

        public string Path
        {
            get
            {
                return this.path!;
            }
            private set
            {
                if (!FileValidator.PathExists(value))
                {
                    throw new InvalidPathException();
                }

                this.path = System.IO.Path.GetFullPath(value);
            }
        }

        public string FullPath
            => System.IO.Path.GetFullPath(this.Path + "/" + this.Name);

        public string Content
            => this.content.ToString();

        public int Size
            => this.content.Length;

        public void Write(string text)
        {
            this.content.Append(text);
        }

        public void WriteLine(string text)
        {
            this.content.AppendLine(text);
        }

        //Writing by chunks -> Memory optimization
        //public void SaveContent()
        //{
        //    string previousContent = File.ReadAllText(this.Path);
        //    string futureContent = previousContent + Environment.NewLine
        //        + this.Content;
        //    File.WriteAllText(this.Path, futureContent);
        //    this.content.Clear();
        //}
    }
}
