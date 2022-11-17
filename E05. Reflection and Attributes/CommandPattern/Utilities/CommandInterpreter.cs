namespace CommandPattern.Utilities
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string cmdName, string[] args)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Type intendedCmdType = assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == $"{cmdName}Command");
            if (intendedCmdType == null)
            {
                throw new InvalidOperationException("Invalid command type!");
            }

            MethodInfo executeMethodInfo = intendedCmdType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(m => m.Name == "Execute");
            if (executeMethodInfo == null)
            {
                throw new InvalidOperationException("Command does not implement required pattern! Try implementing ICommand interface instead!");
            }

            object cmdInstance = Activator.CreateInstance(intendedCmdType);
            string result = (string)executeMethodInfo
                .Invoke(cmdInstance, new object[] { args });

            return result;
        }
    }
}
