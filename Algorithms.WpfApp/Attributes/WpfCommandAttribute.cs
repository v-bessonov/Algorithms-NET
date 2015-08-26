using System;

namespace Algorithms.WpfApp.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WpfCommandAttribute : Attribute
    {

        public WpfCommandAttribute(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}
