using System;

namespace Algorithms.ConsoleApp.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ConsoleCommandAttribute : Attribute
    {
        public ConsoleCommandAttribute(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}
