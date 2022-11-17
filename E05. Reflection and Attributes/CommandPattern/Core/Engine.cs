namespace CommandPattern.Core
{
    using System;
    using System.Linq;

    using Contracts;
    using IO;
    using IO.Contracts;
    using Utilities.Contracts;

    public class Engine : IEngine
    {
        //Will be good to come as arguments...
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly ICommandInterpreter cmdInterpreter;

        private Engine()
        {
            this.reader = new ConsoleReader();
            this.writer = new ConsoleWriter();
        }

        public Engine(ICommandInterpreter commandInterpreter)
            : this()
        {
            this.cmdInterpreter = commandInterpreter;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string[] inputArgs = this.reader.ReadLine()
                        .Split(" ")
                        .ToArray();
                    string cmdName = inputArgs[0];
                    string[] args = inputArgs
                        .Skip(1)
                        .ToArray();

                    string result = this.cmdInterpreter.Read(cmdName, args);
                    this.writer.WriteLine(result);
                }
                catch (InvalidOperationException ioe)
                {
                    this.writer.WriteLine(ioe.Message);
                }
            }
        }
    }
}
